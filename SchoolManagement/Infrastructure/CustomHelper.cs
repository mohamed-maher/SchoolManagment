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
            SecurityEntities db = new SecurityEntities();
            string roles = string.Empty;
            List<tAccessRoleDet> data = db.tAccessRoleDets.Where(m => m.tModule.ModuleName == roleSelector).ToList();
            foreach (tAccessRoleDet r in data)
                roles += r.tModule.ModuleName + ',';
            roles = roles.Substring(1, roles.Length - 1);

            return roles;
        }

        public static bool AddRoles()
        {
            SecurityEntities db = new SecurityEntities();
            var accessrole = (from ac in db.tAccessRoles
                              join userac in db.tUserAccessRoles on ac.AccessRoleID equals userac.AccessRoleID
                              where userac.UserID == SessionPersister.User.UserID
                              select new
                              {
                                  AccessRoleName = ac.AccessRoleName
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