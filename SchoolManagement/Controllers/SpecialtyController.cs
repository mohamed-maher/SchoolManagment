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

        public JsonResult Specialty_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Specialty> specialty = db.Specialties;
            DataSourceResult result = specialty.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Specialty_Create([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Specialties.Add(specialty);
                db.SaveChanges();
            }
            return Json(new { Data = specialty });
        }
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public JsonResult Specialty_Update([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Specialties.Attach(specialty);
                db.Entry(specialty).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            //return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            return Json(ModelState.IsValid ? true : false);
        }
        public ActionResult Specialty_Destroy([DataSourceRequest]DataSourceRequest request, Specialty specialty)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Specialties.Attach(specialty);
                db.Specialties.Remove(specialty);
                db.SaveChanges();
            }
            //return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            return Json(ModelState.IsValid ? true : false);
        }
    }
}