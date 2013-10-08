using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetectServer.Web
{
    [AttributeUsage(AttributeTargets.All)]
    public class IgnoreAuthenticationAttribute : Attribute
    {
        public IgnoreAuthenticationAttribute()
        {
        }
    }
}