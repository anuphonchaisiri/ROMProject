using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary.Model.User
{
    public class UserDataEntity
    {
        public string SID { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string Password { get; set; }
    }
}
