using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Models;

namespace IPDetectServer.Web
{
    public static class SessionManager
    {
        private const string IS_LOGIN = "IS_LOGIN";
        public static bool IsLogin
        {
            get
            {
                //return true;
                var v = HttpContext.Current.Session[IS_LOGIN];
                if (v == null)
                {
                    return false;
                }

                return Convert.ToBoolean(v);
            }
            set 
            {
                HttpContext.Current.Session[IS_LOGIN] = value;
            }
        }

        private const string USER_NAME = "USER_NAME";
        public static UserModel User
        {
            get
            {
                var v = HttpContext.Current.Session[USER_NAME];

                return v as UserModel;
            }
            set
            {
                HttpContext.Current.Session[USER_NAME] = value;
            }
        }
    }
}