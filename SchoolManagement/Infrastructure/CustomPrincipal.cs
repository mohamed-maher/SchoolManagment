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
        SMSEntities db = new SMSEntities();
        private User User;
        public CustomPrincipal(string username)
        {
            this.User = db.Users.Single(u => u.UserName == username);
            this.Identity = new GenericIdentity(username);
        }
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            var accessrole = (from ac in db.AccessRoles
                              from userac in db.UserAccessRoles
                              where userac.User == SessionPersister.User
                              select new
                              {
                                  AccessRoleName = ac.AccessRoleName_en
                              });
            List<string> accessrolename = new List<string>();
            foreach (var ac in accessrole)
                accessrolename.Add(ac.AccessRoleName);
       
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => accessrolename.Contains(r));
        }
    }
}