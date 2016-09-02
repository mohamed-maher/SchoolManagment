using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolManagement.Infrastructure;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class CustomPrincipal : IPrincipal
    {
        SecurityEntities db = new SecurityEntities();
        private tUser User;
        public CustomPrincipal(string username)
        {
            this.User = db.tUsers.Single(u => u.UserName == username);
            this.Identity = new GenericIdentity(username);
        }
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            var accessrole = (from ac in db.tAccessRoles
                              join userac in db.tUserAccessRoles on ac.AccessRoleID equals userac.AccessRoleID
                              where userac.UserID == SessionPersister.User.UserID
                              select new
                              {
                                  AccessRoleName = ac.AccessRoleName
                              });
            List<string> accessrolename = new List<string>();
            foreach (var ac in accessrole)
                accessrolename.Add(ac.AccessRoleName);
       
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => accessrolename.Contains(r));
        }
    }
}