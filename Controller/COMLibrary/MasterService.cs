using COMLibrary.Model.Finance;
using COMLibrary.Model.User;
using COMUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary
{
    public class MasterService
    {
        private DBService db = DBService.getInstance();
        private MasterService()
        {
        }
        private static MasterService _instance;
        public static MasterService getInstance()
        {
            if (_instance == null)
            {
                _instance = new MasterService();
            }
            return _instance;
        }

        #region Master Account

        public List<AccountMsaterModel> searchAccountMaster(string sid,string accountcode)
        {
            string where = "";
            if (!string.IsNullOrEmpty(accountcode))
            {
                where += " and a.account_code='"+ accountcode + @"' ";
            }
            string sql = @"select  a.*
                                ,b.description as created_by_name
                                ,c.description as updated_by_name
                          from COM_MASTER_ACCOUNT a with (nolock)
                            left join COM_AUTH_USER b  with (nolock) on a.created_by = b.userid
                            left join COM_AUTH_USER c  with (nolock) on a.updated_by = c.userid
                          where sid='"+ sid + "' ";
            return db.selectData<AccountMsaterModel>(sql);
        }
        
        #endregion

        #region Employee Userid Master

        public List<EmployeeModel> searchEmployeeMaster(string sid,string employeecode,string active)
        {
            string sql = @"
            SELECT a.sid
                , a.description
                , b.userid
                , c.username
                , c.created_by
                , c.updated_on
            FROM[COM_MASTER_COMPANY] a
           left outer join COM_MASTER_EMPLOYEE_MAPPING b on a.sid = b.sid
            left outer join COM_AUTH_USER c on b.userid = c.userid
            where a.sid = '"+ sid + @"'
            and a.active = '"+ active + "' ";

            List<EmployeeModel> list = db.selectData<EmployeeModel>(sql);
            return list;

        }

        #endregion

    }
}
