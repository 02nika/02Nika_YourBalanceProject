using ClassLibrary1;
using ClassLibrary1.GetData;
using ClassLibrary1.InserOps;
using ClassLibrary1.Login;
using log4net;
using System;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegistrationController : Controller
    {
        private static ILog log = LogManager.GetLogger("RegistrationController");
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegistrationInfo registrationInfo)
        {
            log.Debug($"new registration Attempt: \nBalance:{registrationInfo.Balance}, \nUsername: {registrationInfo.UserName}, \ndescription: {registrationInfo.Desc}");
            if (registrationInfo.Password != registrationInfo.RepeatPassword)
            {
                log.Debug($"password do not match {registrationInfo.Password} - {registrationInfo.RepeatPassword}");
                ViewBag.DoNotMatchmsg = "password do not match!";
                return View();
            }

            if(UserInformation.UserExist(registrationInfo.UserName))
            {
                log.Debug($"Username exist already: {registrationInfo.UserName}");
                ViewBag.DoNotMatchmsg = "UserName Already exist!";
                return View();
            }

            ClassLibrary1.Model.RegistrationInfo registrationInfo1 = new ClassLibrary1.Model.RegistrationInfo()
            {
                UserName = registrationInfo.UserName,
                Password = CheckLoginInfo.ComputeSha256Hash(registrationInfo.Password)
            };

            int UserId = RegisterNewUser.Add(registrationInfo1);
            InsertIntoBalanceTable.InsertB(new UserBalance() { UserId = UserId, Balance = Decimal.Parse(registrationInfo.Balance), Description = registrationInfo.Desc });
            InsertLoginT.InsertToken(Int32.Parse(GetUserIdFromWebConfiguration.GetInfo()));
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index",
            });
        }
    }
}