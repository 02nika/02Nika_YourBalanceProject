using ClassLibrary1;
using ClassLibrary1.InserOps;
using ClassLibrary1.Model;
using ClassLibrary1.Pathing;
using ClassLibrary1.UserSession;
using log4net;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ProfileController : Controller
    {
        private static ILog log = LogManager.GetLogger("HomeController");
        // GET: Profile
        public ActionResult Index()
        {
            int userId = Int32.Parse(GetUserIdFromWebConfiguration.GetInfo());
            if (!UserSession.CheckUserSessionValidation())
            {
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }
            string path = ImageOps.GetImageByUserId(userId);
            if (path != null)
            {
                string[] parts = path.Split('\\');
                ViewBag.Path = "~/" + parts[7] + "/" + parts[8];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + DateTime.Now.ToString("yyyyMMssff") + extension;
                string ImagePath = "~/Image/" + fileName;


                string rightPath = Path.Combine(Server.MapPath("~/Image"), GetPath.ProfileImage(file));

                file.SaveAs(rightPath);
                ImageOps.AddImage(new AddImageModel
                {
                    ImagePath = rightPath,
                    UserId = Int32.Parse(GetUserIdFromWebConfiguration.GetInfo())
                });
            }
                
            return Redirect("profile/index");
        }

        [HttpPost]
        public ActionResult Remove()
        {
            int userId = Int32.Parse(GetUserIdFromWebConfiguration.GetInfo());
            if (!UserSession.CheckUserSessionValidation())
            {
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index",
                });
            }

            string path = ImageOps.RemoveImageByUserId(userId);
            System.IO.File.Delete(path);

            return Redirect("index");
        }
    }
}