using COMLibrary;
using COMLibrary.Model.Finance;
using COMLibrary.Model.User;
using COMUtil;
using ROM.Models;
using ROM.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROM.Controllers
{
    public class MasterController : TemplateController
    {
        private MasterService master = MasterService.getInstance();
        private DBConnect dbcon = new DBConnect();

        public ActionResult User()
        {
            List<UserModel> Model = new List<UserModel>();
            Model.Add(new UserModel()
            {
                UserName = "user 1",
                UserDescription = "user aaa",
                RegionName = "asasasasas",
                CountryName = "asasasasasa",
                StartDate = "12/12/2018"
            });
            Model.Add(new UserModel()
            {
                UserName = "user 2",
                UserDescription = "user bbbb",
                RegionName = "",
                CountryName = "",
                StartDate = "12/12/2018"
            });
            return View(Model);
        }
       
        public ActionResult BudgetPlan()
        {
            return View();
        }

        public ActionResult Employee()
        {
            List<EmployeeModel> listEmp = master.searchEmployeeMaster(AuthUser.SID, "", "");
            return View();
        }

        #region Account Master

        public ActionResult Account()
        {
            List<AccountMsaterModel> listAccount = master.searchAccountMaster(AuthUser.SID, "");
            return View(listAccount);
        }

        [HttpPost]
        public ActionResult AccountCraete(AccountMsaterModel enAccount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    enAccount.sid = AuthUser.SID;
                    enAccount.created_by = AuthUser.UserId;
                    enAccount.created_on = ObjectUtil.getCurrentServerStringDateTime();
                    enAccount.created_on_display = ObjectUtil.Convert2DateTimeDisplay(enAccount.created_on);
                    dbcon.master_account.Add(enAccount);
                    dbcon.SaveChanges();
                    ModelState.Clear();
                    enAccount.created_by_name = AuthUser.FullName;
                    enAccount.updated_by_name = "";
                    return Json(enAccount);
                }
                catch (Exception ex)
                {
                    return ExceptionError(ex.Message);
                }
                
            }
            return Json(enAccount);
        }

        [HttpPost]
        public ActionResult AccountUpdate(AccountMsaterModel enAccount)
        {
            try
            {
                if (enAccount == null || string.IsNullOrEmpty(enAccount.account_code))
                {
                    return ExceptionError("account code for delete is null or empty!!");
                }
                AccountMsaterModel en = dbcon.master_account.Find(AuthUser.SID,enAccount.account_code);
                en.account_name = enAccount.account_name;
                en.updated_by = AuthUser.UserId;
                en.updated_on = ObjectUtil.getCurrentServerStringDateTime();
                en.updated_on_display = ObjectUtil.Convert2DateTimeDisplay(enAccount.updated_on);
                en.updated_by_name = AuthUser.FullName;
                if (TryUpdateModel(en, "", new string[] { "sid", "account_code" }))
                {
                    dbcon.SaveChanges();
                    ModelState.Clear();
                }
                return Json(en);
            }
            catch (Exception ex)
            {
                return ExceptionError(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AccountDelete(AccountMsaterModel enAccount)
        {
            if (enAccount == null || string.IsNullOrEmpty(enAccount.account_code))
            {
                return ExceptionError("account code for delete is null or empty!!");
            }

            try
            {
                var en = dbcon.master_account.Find(AuthUser.SID, enAccount.account_code);
                if (en != null)
                {
                    dbcon.Entry(en).State = EntityState.Deleted;
                    dbcon.SaveChanges();
                    ModelState.Clear();
                    
                }
                return Json(enAccount);
            }
            catch (Exception ex)
            {
                return ExceptionError(ex.Message);
            }
        }


        #endregion
        
    }
}