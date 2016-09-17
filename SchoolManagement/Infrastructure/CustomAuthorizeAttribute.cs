using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;
using System.Web.Routing;

namespace SchoolManagement.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {   
        

        SecurityEntities db = new SecurityEntities();
        //public CustomAuthorizeAttribute(string roleSelector)
        //{
        //    Roles = CustomHelper.GetRoles(roleSelector);
        //}
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                       {
                                           { "action", "Index" },
                                           { "controller", "Login" },
                                       });
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.UserName))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else
            {
                CustomPrincipal mp = new CustomPrincipal(SessionPersister.UserName);
                if (!mp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
                }
            }
        }
        

        
    }
}