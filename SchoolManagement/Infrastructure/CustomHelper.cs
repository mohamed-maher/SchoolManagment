using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Infrastructure
{
    public static class CustomHelper
    {
        public static string GetRoles(string roleSelector)
        {
            SMSEntities db = new SMSEntities();
            string roles = string.Empty;
            //List<AccessRoleDet> data = db.tAccessRoleDets.Where(m => m.tModule.ModuleName == roleSelector).ToList();
            //foreach (tAccessRoleDet r in data)
            //    roles += r.tModule.ModuleName + ',';
            roles = roles.Substring(1, roles.Length - 1);

            return roles;
        }

        public static bool AddRoles()
        {
            SMSEntities db = new SMSEntities();
            var accessrole = (from ac in db.AccessRoles
                              from userac in db.UserAccessRoles 
                              where userac.User == SessionPersister.User
                              select new
                              {
                                  AccessRoleName = ac.AccessRoleName_en
                              });
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser() { UserName = SessionPersister.User.UserName, Id = Convert.ToString(SessionPersister.User.UserID) };
            var hasher = new PasswordHasher();
            //var result = manager.Create(user, Convert.ToString(User.Password));
            foreach (var ac in accessrole)
            {
                ir = rm.Create(new IdentityRole(ac.AccessRoleName));
                manager.AddToRole(user.Id, ac.AccessRoleName);
            }
            return true;
        }
    }
}