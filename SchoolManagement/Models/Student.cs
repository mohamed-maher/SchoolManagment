//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int StudentID { get; set; }
        public int GradeID { get; set; }
        public string StudentName_ar { get; set; }
        public string StudentName_en { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public Nullable<int> SexID { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string FatherName_ar { get; set; }
        public string FatherName_en { get; set; }
        public string MotherName_ar { get; set; }
        public string MotherName_en { get; set; }
        public string FatherProfession { get; set; }
        public string MotherProfession { get; set; }
    
        public virtual Grade Grade { get; set; }
    }
}