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

            //TODO Test login
            if (string.IsNullOrEmpty(AuthUser.UserId))
            {
                AuthUser.Login("romadmin", "1q2w3e4r5t");
            }
            //end
            
            string CompName = Convert.ToString(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["company"]);
            SetSystemIDControl(CompName);
        }

        public ActionResult ExceptionError(string message)
        {
            return new HttpStatusCodeResult(555, message);
        }


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
            ViewBag.CompanyName = company.name;
            ViewBag.CompanyDes = company.description;
            ViewData["MyCompanyInfo"] = listcompany;
        }

        #endregion

    }
}