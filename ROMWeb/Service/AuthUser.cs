using COMLibrary;
using COMLibrary.Model.Company;
using COMLibrary.Model.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROM.Service
{
    public class AuthUser
    {
        
        public AuthUser() { }
        public static bool Login(string username,string password)
        {
            //remove cookie
            HttpContext.Current.Response.Cookies.Remove("COOKIE_ROMWEB_USER_INFO");
            _data = LoginService.getInstance().Login("COM",username,password);
            if (_data == null || string.IsNullOrEmpty(_data.Username))
            {
                return false;
            }
            //add cookie
            HttpCookie cookName = new HttpCookie("COOKIE_ROMWEB_USER_INFO");
            cookName.Value = JsonConvert.SerializeObject(_data);
            HttpContext.Current.Response.Cookies.Add(cookName);
            return true;
        }

        public static bool SetAuthPublic(CompanyInfoModel company) {
            HttpContext.Current.Response.Cookies.Remove("COOKIE_ROMWEB_USER_INFO");
            HttpCookie cookName = new HttpCookie("COOKIE_ROMWEB_USER_INFO");
            _data = new UserDataEntity();
            _data.SID = company.sid;
            cookName.Value = JsonConvert.SerializeObject(_data);
            HttpContext.Current.Response.Cookies.Add(cookName);
            return true;
        }

        private static UserDataEntity _data { get; set; }
        private static UserDataEntity data
        {
            get
            {
                if (_data == null || string.IsNullOrEmpty(_data.SID))
                {
                    if (HttpContext.Current.Request.Cookies["Cookie_AUTH_URL"] != null)
                    {
                        _data = JsonConvert.DeserializeObject<UserDataEntity>(HttpContext.Current.Request.Cookies["COOKIE_ROMWEB_USER_INFO"].Value);
                    }
                    else
                    {
                        _data = new UserDataEntity();
                    }
                }
                return _data;
            }
        }

        public static string SID { get { return data.SID; } }
        public static string UserId { get { return data.UserId; } }
        public static string Username { get { return data.Username; } }
        public static string FullName { get { return data.FullName; } }
        public static string EmployeeCode { get { return data.EmployeeCode; } }

        
    }
}