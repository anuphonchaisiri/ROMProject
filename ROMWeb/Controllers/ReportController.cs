﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROM.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }
    }
}