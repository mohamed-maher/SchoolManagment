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
    public class StudentController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            ViewData["Gender"] = db.Genders.ToList();
            ViewData["Grade"] = db.Grades.ToList();
            return View();
        }
        public JsonResult Student_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Student> Student = db.Students;
            DataSourceResult result = Student.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Student_Create([DataSourceRequest]DataSourceRequest request, Student student)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.Entry(student).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            return Json(new { Data = student });
            //return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Student_Update([DataSourceRequest]DataSourceRequest request, Student student)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DataSourceResult result = null;
            if (ModelState.IsValid)
            {
                db.Students.Attach(student);
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        public ActionResult Student_Destroy([DataSourceRequest]DataSourceRequest request, Student student)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Students.Attach(student);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}