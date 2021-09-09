using ClassLibrary1;
using log4net;
using System;
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