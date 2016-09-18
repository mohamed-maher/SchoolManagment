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
    public class SpecialtyController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Staffs_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Specialty> specialty = db.Specialties;
            DataSourceResult result = specialty.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Staffs_Create([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Specialties.Add(specialty);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Staffs_Update([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Specialties.Attach(specialty);
                db.Entry(specialty).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        public ActionResult Staffs_Destroy([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Specialties.Attach(specialty);
                db.Specialties.Remove(specialty);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}