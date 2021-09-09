﻿using ClassLibrary1;
using ClassLibrary1.InserOps;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BalanceController : Controller
    {
        private static ILog log = LogManager.GetLogger("BalanceController");
        // GET: Balance
        public ActionResult Index()
        {
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }
            return View();
        }

        public ActionResult SecondPage()
        {
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }

            List<UserBalance> UB = ConnectionWithDb.GetInfoFromDB(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo()));

            return View(UB);
        }
        public ActionResult Add()
        {
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add(Balance b)
        {
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }

            UserBalance uB = new UserBalance() { Balance = b.InputBalance, Description = b.Description, UserId = Int32.Parse(GetUserIdFromWebConfiguration.GetInfo()) };
            InsertIntoBalanceTable.ChangeBalance(uB);

            log.Debug($"Users Balance and description Changed: \nbalance: {uB.Balance}\ndescription: {uB.Description}.");
            return Redirect("SecondPage");
        }

    }
}