using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SchoolManagement.Infrastructure;
using SchoolManagement.Models;
using SchoolManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        SecurityEntities db = new SecurityEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel loginData)
        {
            if (ModelState.IsValid)
            {
                List<tUser> n = db.tUsers.Where(u => u.UserName == loginData.UserName).ToList();
                List<tUser> p = db.tUsers.Where(u => u.Password.ToString() == loginData.Password).ToList();
                tUser user = db.tUsers.Single(u => u.UserName == loginData.UserName && u.Password.ToString() == loginData.Password);
                if (user == null)
                {
                    SessionPersister.UserName = string.Empty;
                    SessionPersister.User = null;
                    ModelState.AddModelError("", "Account's Invalid !");
                    return View("Index");
                }
                else
                {
                    SessionPersister.UserName = user.UserName;
                    SessionPersister.User = user;
                    CustomPrincipal cp = new CustomPrincipal(user.UserName);
                    CustomHelper.AddRoles();
                    return RedirectToAction("Index", "Modules");
                }
            }
            else
            {
                ModelState.AddModelError("", "password incorrect !");
            }
            return View("Index");
        }

        //public ActionResult Logout()
        //{
        //    var signinManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        //    signinManager.AuthenticationManager.SignOut();

        //    return RedirectToAction("Login");
        //}
    }
}