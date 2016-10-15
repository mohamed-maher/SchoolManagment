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
    public class SubjectController : Controller
    {
        SMSEntities db = new SMSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int? id)
        {
            ViewData["Employees"] = db.Staffs.Select(s => new { EmployeeID = s.EmployeeID, Name = s.FirstName_ar + s.LastName_ar });
            ViewData["Grade"] = db.Grades.ToList();
            if(id != null)
            {
                Subject subject = db.Subjects.Single(q => q.SubjectsID == id);
                return View(subject);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(string SubjectID, string Subjects_ar, string Subjects_en)
        {
            Subject s = new Subject();
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                s.SubjectsID = id;
                s.Subjects_ar = Subjects_ar;
                s.Subjects_en = Subjects_en;
                db.Subjects.Attach(s);
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                s.Subjects_ar = Subjects_ar;
                s.Subjects_en = Subjects_en;
                db.Subjects.Add(s);
                db.Entry(s).State = System.Data.Entity.EntityState.Added;
            }
            db.SaveChanges();
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Subjects_Read([DataSourceRequest]DataSourceRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Subject> subject = db.Subjects.ToList();
            DataSourceResult result = subject.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubjectsDet_Read([DataSourceRequest]DataSourceRequest request, string SubjectID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubjectModel> data = new List<SubjectModel>();
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                List<GradeSub> Subject = db.GradeSubs.Include("Staff").Where(s => s.SubjectsID == id).ToList();
                foreach (GradeSub gs in Subject)
                {
                    int index = data.FindIndex(s => s.GradeID == gs.GradeID && s.SubjectsID == gs.SubjectsID);
                    if (index != -1)
                    {
                        data[index].Employees.Add(new Staff()
                        {
                            EmployeeID = gs.EmployeeID,
                            FirstName_ar = gs.Staff.FirstName_ar,
                            FirstName_en = gs.Staff.FirstName_en,
                            LastName_ar = gs.Staff.LastName_ar,
                            LastName_en = gs.Staff.LastName_en
                        });
                    }
                    else
                    {
                        SubjectModel s = new SubjectModel();
                        s.SubjectsID = gs.SubjectsID;
                        s.GradeID = gs.GradeID;
                        s.Employees = new List<Staff>() { new Staff() { EmployeeID = gs.EmployeeID, FirstName_ar = gs.Staff.FirstName_ar, FirstName_en = gs.Staff.FirstName_en,
                                    LastName_ar = gs.Staff.LastName_ar, LastName_en = gs.Staff.LastName_en } };
                        data.Add(s);
                    }
                }
            }
            DataSourceResult result = data.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubjectsDet_Create([DataSourceRequest]DataSourceRequest request, SubjectModel subject, string SubjectID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                if (ModelState.IsValid && id != null)
                {
                    subject.SubjectsID = id;
                    GradeSub gs = new GradeSub();
                    gs.SubjectsID = id;
                    gs.GradeID = subject.GradeID;
                    for (int i = 0; i < subject.Employees.Count; i++)
                    {
                        int empid = subject.Employees[i].EmployeeID;
                        var data = db.GradeSubs.Include("Staff").FirstOrDefault(q => q.SubjectsID == id && q.GradeID == gs.GradeID && q.Staff.EmployeeID == empid);
                        if (data == null)
                        {
                            gs.EmployeeID = subject.Employees[i].EmployeeID;
                            subject.Employees[i] = db.Staffs.Single(q => q.EmployeeID == empid);
                            db.Entry(gs).State = System.Data.Entity.EntityState.Added;
                            db.SaveChanges();
                        }
                    }
                }
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubjectsDet_Update([DataSourceRequest]DataSourceRequest request, SubjectModel subject, string SubjectID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                if (ModelState.IsValid)
                {
                    List<GradeSub> data = db.GradeSubs.Include("Staff").Where(s => s.GradeID == subject.GradeID && s.SubjectsID == id).ToList();
                    for (int i = 0; i < subject.Employees.Count; i++)
                    {
                        int empid = subject.Employees[i].EmployeeID;
                        int index = data.FindIndex(s => s.Staff.EmployeeID == empid);
                        if (index == -1)
                        {
                            GradeSub gs = new GradeSub();
                            gs.SubjectsID = subject.SubjectsID;
                            gs.GradeID = subject.GradeID;
                            gs.EmployeeID = subject.Employees[i].EmployeeID;
                            subject.Employees[i] = db.Staffs.Single(q => q.EmployeeID == empid);
                            db.Entry(gs).State = System.Data.Entity.EntityState.Added;
                            db.SaveChanges();
                        }
                    }
                    foreach (GradeSub gs in data)
                    {
                        int empid = gs.EmployeeID;
                        int index = subject.Employees.FindIndex(s => s.EmployeeID == empid);
                        if (index == -1)
                        {
                            db.GradeSubs.Attach(gs);
                            db.GradeSubs.Remove(gs);
                        }
                    }
                    db.SaveChanges();
                }
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        public ActionResult SubjectsDet_Destroy([DataSourceRequest]DataSourceRequest request, SubjectModel subject, string SubjectID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                if (ModelState.IsValid)
                {
                    List<GradeSub> data = db.GradeSubs.Where(s => s.GradeID == subject.GradeID && s.SubjectsID == id).ToList();
                    foreach (GradeSub gs in data)
                    {
                        db.GradeSubs.Attach(gs);
                        db.GradeSubs.Remove(gs);
                    }
                    db.SaveChanges();
                }
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        public ActionResult Delete(string SubjectID)
        {
            if (SubjectID != "")
            {
                int id = Convert.ToInt32(SubjectID);
                List<GradeSub> data = db.GradeSubs.Where(q => q.SubjectsID == id).ToList();
                foreach (GradeSub gs in data)
                {
                    db.GradeSubs.Attach(gs);
                    db.GradeSubs.Remove(gs);
                }
                db.SaveChanges();

                Subject s = db.Subjects.Single(q => q.SubjectsID == id);
                db.Subjects.Attach(s);
                db.Subjects.Remove(s);
                db.SaveChanges();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}