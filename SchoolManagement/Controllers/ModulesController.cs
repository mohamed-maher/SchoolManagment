using SchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        [CustomAuthorize(Roles = "AccessRole")]
        public ActionResult Index()
        {
            return View();
        }
    }
}