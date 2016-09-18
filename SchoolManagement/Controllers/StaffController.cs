using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace SchoolManagement.Controllers
{
    public class StaffController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            ViewData["Specialty"] = db.Specialties.ToList();
            ViewData["Gender"] = db.Genders.ToList();
            return View();
        }

        public JsonResult Staffs_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Staff> staffs = db.Staffs;
            DataSourceResult result = staffs.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Staffs_Create([DataSourceRequest]DataSourceRequest request, Staff staff)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Staffs_Update([DataSourceRequest]DataSourceRequest request, Staff staff)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Staffs.Attach(staff);
                db.Entry(staff).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        public ActionResult Staffs_Destroy([DataSourceRequest]DataSourceRequest request, Staff staff)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Staffs.Attach(staff);
                db.Staffs.Remove(staff);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}