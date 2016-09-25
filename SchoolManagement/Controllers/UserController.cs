using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class UserController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<User> users = db.Users.Include("Staff").ToList();
            return View(users);
        }
        public ActionResult Create()
        {
            ViewData["Employees"] = db.Staffs.Select(s => new { EmployeeID = s.EmployeeID, Name = s.FirstName_ar + s.LastName_ar });
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel um)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = um.UserName;
                user.Password = um.Password;
                user.EMail = um.Email;
                user.EmployeeID = um.EmployeeID;
                db.Users.Add(user);
                db.SaveChanges();
                return View("Index");
            }
            return View();
        }
    }
}