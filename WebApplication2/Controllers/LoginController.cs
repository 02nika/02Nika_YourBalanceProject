using ClassLibrary1;
using ClassLibrary1.InserOps;
using ClassLibrary1.Login;
using ClassLibrary1.Model;
using log4net;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private static ILog log = LogManager.GetLogger("LoginController");

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginInfo Lf)
        {
            if (ModelState.IsValid)
            {
                UserInformation Ui = CheckLoginInfo.UserExist(Lf.UserName, Lf.Password);
                if (Ui == null)
                {
                    log.Debug($"User doesn't exist. \nused username: {Lf.UserName}\npassword: {Lf.Password}");
                    return RedirectToRoute(new
                    {
                        controller = "Login",
                        action = "Index",
                    });
                }

                int userId = InsertLoginT.InsertToken(Ui.UserId);

                GetUserIdFromWebConfiguration.SetInfo(Ui.UserId);
                log.Debug($"UserId with new Session: {Ui.UserId}");
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }
            log.Debug("Login modelstate is not valid");
            return View();
        }
    }
}