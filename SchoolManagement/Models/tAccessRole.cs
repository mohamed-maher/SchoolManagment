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
    
    public partial class tAccessRole
    {
        public tAccessRole()
        {
            this.tAccessRoleDets = new HashSet<tAccessRoleDet>();
            this.tUserAccessRoles = new HashSet<tUserAccessRole>();
        }
    
        public int AccessRoleID { get; set; }
        public string AccessRoleName { get; set; }
    
        public virtual ICollection<tAccessRoleDet> tAccessRoleDets { get; set; }
        public virtual ICollection<tUserAccessRole> tUserAccessRoles { get; set; }
    }
}
