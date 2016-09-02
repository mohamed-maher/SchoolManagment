using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Infrastructure
{
    public class SessionPersister
    {
        static string usernameSession = string.Empty;
        public static tUser User { get; set; }
        public static string UserName
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var session = HttpContext.Current.Session[usernameSession];
                if (session != null)
                    return session as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSession] = value;
            }
        }
    }
}