﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUnityBootstrapperTest.Business;

namespace MvcUnityBootstrapperTest.Controllers
{
    public class HomeController : Controller
    {          
        private readonly Business.IBusinessClass _businessClass;
        private readonly Business.IBusinessClass2 _businessClass2;

        public HomeController(Business.IBusinessClass businessClass, Business.IBusinessClass2 businessClass2)
        {
            _businessClass = businessClass;
            _businessClass2 = businessClass2;
        }

        public ActionResult Index()
        {
            _businessClass.Hello();
            _businessClass2.Hello();
            _businessClass2.SayHello("conHello");
            _businessClass.Hello();
            //throw new Exception("Hello");
            return View();
        }
    }
}
