using ClassLibrary1;
using ClassLibrary1.GetData;
using ClassLibrary1.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private static ILog log = LogManager.GetLogger("HomeController");

        public ActionResult Index()
        {
            log.Debug("Home Page!!!");
            if (GetUserIdFromWebConfiguration.CheckTokenValidation(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())) == 0)
            {
                GetUserIdFromWebConfiguration.ClearInfo();
                log.Debug($"there is no session. redirecting to login page.");
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string SearchValue)
        {
            List<SearchUser> searchResult = ClassLibrary1.GetData.UserInformation.GetSearchResult(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo()), SearchValue);

            if (searchResult != null)
            {
                string[] parts;
                for (int i = 0; i < searchResult.Count; i++)
                {
                    parts = searchResult[i].ImagePath.Split('\\');
                    searchResult[i].ImagePath = "~/" + parts[7] + "/" + parts[8];
                }
            }

            ViewBag.searchResult = searchResult;
            return View();
        }

        public ActionResult About()
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

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
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

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}