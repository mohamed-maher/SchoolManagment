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
        SMSEntities db = new SMSEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                ViewBag.Error = "Please Enter User Name";
            else if (string.IsNullOrEmpty(Convert.ToString(password)))
                ViewBag.Error = "Please Enter Password";
            else
            {
                List<User> n = db.Users.Where(u => u.UserName == username).ToList();
                List<User> p = db.Users.Where(u => u.Password == password).ToList();
                User user = db.Users.Single(u => u.UserName == username && u.Password == password);
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
                    return RedirectToAction("Index", "Staff");
                }
            }
            return View("Index");
        }
        public JsonResult SelectIDs()
        {
            return Json(10, JsonRequestBehavior.AllowGet);
        }
    }
}