using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMUtil
{
    public class DBService
    {
        private String DBCenterConnectionString { get { return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DBCenterConnectionString"]); } }
        //private String DBLocalConnectionString { get { return Convert.ToString(System.Web.HttpContext.Current.Session["DBLocalConnectionString"]); } }
        private String DBLocalConnectionString { get { return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DBCenterConnectionString"]); } }

        private DBService()
        {
        }
        private static DBService _instance;
        public static DBService getInstance()
        {
            if (_instance == null)
            {
                _instance = new DBService();
            }
            return _instance;
        }

        #region DB Local

        public List<T> selectData<T>(String sql)
        {
            return selectData<T>(sql, DBLocalConnectionString);
        }
        
        public void executeSQL(String sql)
        {
            List<String> listQuery = new List<String>();
            executeSQL(listQuery, DBLocalConnectionString);
            listQuery = null;
        }

        public void executeSQL(List<String> ListQuery)
        {
            executeSQL(ListQuery, DBLocalConnectionString);
        }

        #endregion

        #region DB Center
        public List<T> selectCenterData<T>(String sql)
        {
            return selectData<T>(sql, DBCenterConnectionString);
        }
        public void executeCenterSQL(String sql)
        {
            List<String> listQuery = new List<String>();
            executeSQL(listQuery, DBLocalConnectionString);
            listQuery = null;
        }

        public void executeCenterSQL(List<String> ListQuery)
        {
            executeSQL(ListQuery, DBLocalConnectionString);
        }
        #endregion
        
        #region Private Properties

        private List<T> selectData<T>(String sql, String ConnectionString)
        {
            List<T> list = new List<T>();
            DataTable dt = new DataTable();
            OleDbConnection cn = null;
            try
            {
                cn = getConnectedConnection(ConnectionString);
                OleDbCommand sqlCommand = new OleDbCommand(sql, cn);
                OleDbDataAdapter da = new OleDbDataAdapter(sqlCommand);
                da.Fill(dt);
                da.Dispose();
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                };
                list = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dt), settings);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn != null)
                {
                    closeConnection(cn);
                    dt = null;
                    GC.Collect();
                }
            }

            return list;

        }
        private void executeSQL(List<String> listQuery, String ConnectionString)
        {
            using (OleDbConnection connection =
               new OleDbConnection(ConnectionString))
            {
                OleDbCommand command = new OleDbCommand();
                OleDbTransaction transaction = null;
                // Set the Connection to the new OleDbConnection.
                command.Connection = connection;
                // Open the connection and execute the transaction.
                try
                {
                    connection.Open();
                    // Start a local transaction with ReadCommitted isolation level.
                    transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    // Assign transaction object for a pending local transaction.
                    command.Connection = connection;
                    command.Transaction = transaction;
                    listQuery.ForEach(r =>
                    {
                        // Execute the commands.
                        command.CommandText = r;
                        command.ExecuteNonQuery();
                    });

                    // Commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {
                    }
                    throw ex;
                }
                finally
                {
                    command.Dispose();
                    closeConnection(connection);
                    GC.Collect();
                }
            }
        }
        private OleDbConnection getConnectedConnection(String ConnectionString)
        {
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        private void closeConnection(OleDbConnection conn)
        {
            conn.Close();
            conn.Dispose();
        }

        #endregion
    }
}
