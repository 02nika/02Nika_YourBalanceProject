using ClassLibrary1;
using ClassLibrary1.Change;
using ClassLibrary1.Login;
using log4net;
using System;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ForgetPasswordController : Controller
    {
        private static ILog log = LogManager.GetLogger("ForgetPasswordController");
        // GET: ForgetPassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ForgetEmailFormModel FEFM)
        {
            if(FEFM.NewPassword != FEFM.RepeatNewPassword)
            {
                ViewBag.DoNotMatchmsg = "password do not match!";
                return View();
            }

            int UserId = UserEquality.CheckUserName(FEFM.UserName);
            if (UserId != 0)
            {
                GetUserIdFromWebConfiguration.SetInfo(UserId);
                ChangeData.ChangeUserPassword(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo()),
                                              FEFM.NewPassword);
                GetUserIdFromWebConfiguration.SetInfo(UserId);
                log.Info($"User {FEFM.UserName} now changed password.");
                return Redirect("Home/Index");
            }
            return Redirect("ForgetPassword/Index");
        }

    }
}