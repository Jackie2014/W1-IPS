using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPDetectServer.Web
{
    public class AcceptButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public AcceptButtonAttribute(string name)
        {
            this.Name = name;
        }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }

            bool result = controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
            
            if (result == false)
            {
                result = controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name + ".x")
                    && controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name + ".y");
            }

            return result;
        }
    }
}
