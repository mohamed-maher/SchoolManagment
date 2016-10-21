using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class SubjectModel
    {
        public int SubjectsID { get; set; }
        public string Subjects_ar { get; set; }
        public string Subjects_en { get; set; }
        public int GradeID { get; set; }
        [ForeignKey("GradeID")]
        public Grade grade { get; set; }
        //[UIHint("StaffEditor")]
        public List<Staff> Employees { get; set; }
    }
}