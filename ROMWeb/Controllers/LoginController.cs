using ROM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROM.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("Username or passsword is valid!!");
                }

                bool ischeck = AuthUser.Login(username, password);
                if (!ischeck)
                {
                    throw new Exception("Username or passsword is valid!!");
                }
                System.Web.HttpContext.Current.Response.Redirect("~/" + AuthUser.Company, true);
                return View();
            }
            catch (Exception ex)
            {
               ViewBag.MessageError = ex.Message;
               return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            HttpCookie cookName = new HttpCookie(ConstApplication.COOKIE_USER_INFO);
            cookName.Value = "";
            System.Web.HttpContext.Current.Response.Cookies.Add(cookName);

            System.Web.HttpContext.Current.Response.Redirect("~/" + AuthUser.Company+"/login", true);
            return View();
        }
    }
}