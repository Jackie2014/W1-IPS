using IPDetectServer.Repositories;
using IPDetectServer.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Lib.Common;
using IPDetectServer.Business;
using IPDetectServer.Models;
using System.Collections.Specialized;

namespace IPDetectServer.Web.Controllers
{
    [IgnoreAuthentication]
    public class HomeController : CustomizedControllerBase
    {
        public ActionResult Index()
        {
            SessionManager.IsLogin = false;
            SessionManager.User = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginRequest loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(String.IsNullOrEmpty(loginModel.UserName) || String.IsNullOrEmpty(loginModel.Password))
                    {
                        ModelState.AddModelError("", "请输入用户名和密码!");
                        return View(loginModel);
                    }

                    UserBusiness userBiz = new UserBusiness();
                    UserModel userModel = null;
                    bool isLoginSuccess = userBiz.Login(loginModel.UserName, loginModel.Password, out userModel,true, Context.ClientIP);

                    if (isLoginSuccess)
                    {
                        SessionManager.IsLogin = true;
                        SessionManager.User = userModel;
                        string redirectUrl = Request.QueryString["returnUrl"];
                        if (!String.IsNullOrWhiteSpace(redirectUrl))
                        {
                            return Redirect(redirectUrl);
                        }
                        else
                        {
                            return RedirectToAction("MyInfo", "User");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名或密码不正确!");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(loginModel);

        }

        [HttpPost]
        [AcceptButton("Cancel")]
        public ActionResult Cancel()
        {
            return View();
        }
    }

  
}
