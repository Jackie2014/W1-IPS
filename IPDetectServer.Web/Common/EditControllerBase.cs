using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPDetectServer.Web
{
    public abstract class EditControllerBase<T> : CustomizedControllerBase
    {
        public abstract ActionResult SaveAction(T viewModel);
    }
}