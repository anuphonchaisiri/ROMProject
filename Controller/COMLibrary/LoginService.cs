using COMLibrary.Model.User;
using COMUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary
{
    public class LoginService
    {
        private static LoginService _instance;
        public static LoginService getInstance()
        {
            if (_instance == null)
            {
                _instance = new LoginService();
            }
            return _instance;
        }
        
        public UserDataEntity Login(string sidname, string Username, string Password)
        {
            Password = Decryptor.Decryptor.encrypt(Password);
            string sql = @"
                SELECT a.sid as SID,
	                c.userid as UserId,
	                c.username as Username,
	                c.description as FullName,
	                c.userid as EmployeeCode,
                    c.password as Password
	                FROM COM_MASTER_COMPANY a with (nolock)
	                inner join COM_MASTER_EMPLOYEE_MAPPING b with (nolock) on a.sid = b.sid
	                inner join COM_AUTH_USER c with (nolock) on b.userid = c.userid
	                where a.active = 1  and b.active = 1 
	                and a.name ='" + sidname + @"' 
	                and c.username='"+ Username + @"'
	                and password='"+ Password + @"'
            ";
            List<UserDataEntity> listd = DBService.getInstance().selectCenterData<UserDataEntity>(sql);
            UserDataEntity da = listd.Find(x => x.Username == Username && x.Password == Password);
            UserDataEntity d = new UserDataEntity();
            if (da != null)
            {
                d.SID = da.SID;
                d.UserId = da.UserId;
                d.Username = da.Username;
                d.FullName = da.FullName;
                d.EmployeeCode = da.EmployeeCode;
            }
            da = null;
            listd = null;
            return d;
        }
    }
}
