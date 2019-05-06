using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROM.Controllers
{
    public class CompanyController : TemplateController
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
    }
}