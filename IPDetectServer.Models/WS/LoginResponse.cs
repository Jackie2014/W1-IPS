﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace IPDetectServer.Models
{
 
    public class LoginResponse
    {
        public string Token
        {
            get;
            set;
        }

        public bool IsAdministrator
        {
            get;
            set;
        }
    }
}