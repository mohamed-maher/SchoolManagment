using SchoolManagement.Infrastructure;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class LoginController : Controller
    {
        SecurityEntities db = new SecurityEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View("Index", "_Layout");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, int password)
        {
            if (string.IsNullOrEmpty(username))
                ViewBag.Error = "Please Enter User Name";
            else if (string.IsNullOrEmpty(Convert.ToString(password)))
                ViewBag.Error = "Please Enter Password";
            else
            {
                List<tUser> n = db.tUsers.Where(u => u.UserName == username).ToList();
                List<tUser> p = db.tUsers.Where(u => u.Password == password).ToList();
                tUser user = db.tUsers.Single(u => u.UserName == username && u.Password == password);
                if (user == null)
                {
                    SessionPersister.UserName = string.Empty;
                    SessionPersister.User = null;
                    ViewBag.Error = "Account's Invalid";
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
            return View("Index");
        }
    }
}