﻿using bwarrickShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bwarrickShoppingApp.Controllers
{
    public class HomeController : Universal
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Subscription()
        {
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult Hiring()
        {
            return View();
        }
        public ActionResult Classes()
        {
            return View();
        }
    }
}