using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetectServer.Web.Models
{
    public class ChangePasswordViewModel
    {
        public string LoginName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string NewPassword
        {
            get;
            set;
        }

        public string PasswordConfirm
        {
            get;
            set;
        }
    }
}