using COMLibrary;
using COMLibrary.Model.Finance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ROM.Service
{
    public class DBConnect : DbContext
    {
       // public String DBCenterConnectionString { get { return Convert.ToString("Provider=SQLOLEDB.1;" + System.Configuration.ConfigurationManager.ConnectionStrings["DBCenterConnectionStringEntity"]); } }
       // public String DBLocalConnectionString { get { return Convert.ToString(System.Web.HttpContext.Current.Session["DBLocalConnectionString"]); } }
       
        public DBConnect() : base("DBCenterConnectionStringEntity")
        {
            Database.SetInitializer<DBConnect>(new CreateDatabaseIfNotExists<DBConnect>());
        }
        
        public DbSet<AccountMsaterModel> master_account { get; set; }
        
    }
}