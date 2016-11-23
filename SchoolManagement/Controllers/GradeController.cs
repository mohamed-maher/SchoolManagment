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
    public class GradeController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Grade_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Grade> Grade = db.Grades;
            DataSourceResult result = Grade.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Grade_Create([DataSourceRequest]DataSourceRequest request, Grade Grade)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Grades.Add(Grade);
                db.SaveChanges();
            }
            return Json(new { Data = Grade });
        }
        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public JsonResult Grade_Update([DataSourceRequest]DataSourceRequest request, Grade Grade)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Grades.Attach(Grade);
                db.Entry(Grade).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            //return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            return Json(ModelState.IsValid ? true : false);
        }
        public ActionResult Grade_Destroy([DataSourceRequest]DataSourceRequest request, Grade Grade)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Grades.Attach(Grade);
                db.Grades.Remove(Grade);
                db.SaveChanges();
            }
            //return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            return Json(ModelState.IsValid ? true : false);
        }
    }
}