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
    
    public partial class tUser
    {
        public tUser()
        {
            this.tUserAccessRoles = new HashSet<tUserAccessRole>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; }
        public int EmployeeID { get; set; }
    
        public virtual tEmployee tEmployee { get; set; }
        public virtual ICollection<tUserAccessRole> tUserAccessRoles { get; set; }
    }
}
