using COMLibrary;
using COMLibrary.Model.Company;
using ROM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROM.Controllers
{
    public class TemplateController : Controller
    {
        public TemplateController()
        {

            if (System.Web.HttpContext.Current.Request.RequestType == "POST")
            {
                return;
            }

            string CompName = AuthUser.Company;
            if (string.IsNullOrEmpty(AuthUser.UserId))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/"+ CompName + "/Login", false);
                return;
            }
            
            SetSystemIDControl(CompName);
        }

        //ใช้การณี ajax call API
        public ActionResult ExceptionError(string message)
        {
            return new HttpStatusCodeResult(555, message);
        }

        #region //ใช้กรณี Alert ออกทางหน้าจอ ***หน้าจอ view botton submit form

        public ActionResult AlertSuccess(string message)
        {
            ViewBag.MessageSuccess = message;
            return View();
        }

        public ActionResult AlertError(string message)
        {
            ViewBag.MessageError = message;
            return View();
        }

        public ActionResult AlertInfo(string message)
        {
            ViewBag.MessageInfo = message;
            return View();
        }

        public ActionResult AlertRedirect(string message,string urlRedirect)
        {
            ViewBag.MessageRedirect = message;
            ViewBag.UrlRedirect = urlRedirect;
            return View();
        }
        
        #endregion

        #region Private  Set Value

        private void SetSystemIDControl(string CompName)
        {
            List<CompanyInfoModel> listcompany = new List<CompanyInfoModel>();
            //get Select Company
            CompanyInfoModel company = new CompanyInfoModel();
            if (!string.IsNullOrEmpty(AuthUser.UserId))
            {
                listcompany = CompanyService.getInstance().searchCompamyInfo(AuthUser.UserId, "", "");
                if (listcompany != null && listcompany.Count > 0)
                {
                    if (!string.IsNullOrEmpty(CompName))
                    {
                        company = listcompany.Find(x => x.name.ToLower().Equals(CompName.ToLower()));
                    }
                    else
                    {
                        company = listcompany.FirstOrDefault();
                    }
                }
            }
            //Set System ID
            if (company == null || string.IsNullOrEmpty(company.sid))
            {
                listcompany = CompanyService.getInstance().searchCompamyInfoRefSystem("", CompName);
                if (listcompany.Count > 0) {
                    company = listcompany[0];
                    AuthUser.SetAuthPublic(company);
                }
            }
            if (company != null)
            {
                ViewBag.CompanyName = company.name;
                ViewBag.CompanyDes = company.description;
                ViewData["MyCompanyInfo"] = listcompany;
            }
            else
            {
                ViewBag.MessageRedirect = "ไม่พบองค์กรที่ระบุ!!";
                ViewBag.UrlRedirect = "/";
            }
        }

        #endregion

    }
}