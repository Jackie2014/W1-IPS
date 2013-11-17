using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Web.Models;
using IPDetectServer.Business;
using IPDetectServer.Models;
using IPDetectServer.Repositories;

namespace IPDetectServer.Web.Controllers
{
    public class UserController : CustomizedControllerBase
    {
        //
        // GET: /UserGrant/

        public ActionResult Index()
        {
            return MyInfo();
            //return View();
        }

        public ActionResult New()
        {
            return View(new UserViewModel());
        }

        public ActionResult MyInfo()
        {
            try
            {
                var myInfo = new UserViewModel(SessionManager.User);
                return View("MyInfo", myInfo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("MyInfo", new UserViewModel());
            }
        }

        public ActionResult Manage()
        {
            try
            {
                var userList = new UserListViewModel();
                userList.QueryData();
                userList.CountTotalRows();
                return View("Manage", userList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Manage", new UserListViewModel());
            }
        }

        [HttpPost]
        [AcceptButton("btnEditUserInManage")]
        public ActionResult EditUserInManage(UserListViewModel model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.EditName) || string.IsNullOrWhiteSpace(model.EditCity))
                {
                    throw new Exception("真实姓名和城市不能为空。");
                }

                User user = new User{
                    UserName = model.SelectedLoginName,
                    FullName = model.EditName,
                    Phone = model.EditPhone,
                    City = model.EditCity,
                    Description = model.EditDescription
                };

                UserRepository ur = new UserRepository();
                ur.UpdateUser(user);
                model.QueryData();
                return View("Manage", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [AcceptButton("btnPageNavigate")]
        public ActionResult PagingAction(UserListViewModel model)
        {
            try
            {
                string pageIndex = Request.Form["txtPageNaviNo"];
                if (!string.IsNullOrWhiteSpace(pageIndex))
                {
                    model.PageIndex = int.Parse(pageIndex);
                }
                else
                {
                    model.PageIndex = 0;
                }

                model.QueryData();
                //model.CountTotalRows();
                return View("Manage", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult Password()
        {
            try
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel
                {
                    LoginName = SessionManager.User.LoginName
                };
                return View("Password", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Password", new ChangePasswordViewModel());
            }
        }

        [HttpPost]
        [AcceptButton("btnChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.Password)
                    || String.IsNullOrWhiteSpace(model.NewPassword)
                    || String.IsNullOrWhiteSpace(model.PasswordConfirm))
                {
                    throw new Exception("打 * 的为必填字段，请正确填写完毕再提交。");
                }

                if (!model.NewPassword.Equals(model.PasswordConfirm))
                {
                    throw new Exception("两次输入的密码不一致，请重新输入。");
                }

                UserBusiness ub = new UserBusiness();
                ub.ChangePassword(SessionManager.User.LoginName, model.Password, model.NewPassword);

                ModelState.AddModelError("", "密码修改成功！");
                return View("Password", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Password", model);
            }
        }

        [HttpPost]
        [AcceptButton("btnChangePasswordReset")]
        public ActionResult ChangePasswordReset()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                LoginName = SessionManager.User.LoginName
            };
            return View("Password", model);
        }

        [HttpPost]
        [AcceptButton("btnAddUser")]
        public ActionResult AddUser(UserViewModel model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.LoginName)
                    || String.IsNullOrWhiteSpace(model.Password)
                    || String.IsNullOrWhiteSpace(model.PasswordConfirm)
                    || String.IsNullOrWhiteSpace(model.Name)
                    || String.IsNullOrWhiteSpace(model.City))
                {
                    throw new Exception("打 * 的为必填字段，请正确填写完毕再提交。");
                }
                
                if(!model.Password.Equals(model.PasswordConfirm))
                {
                    throw new Exception("两次输入的密码不一致，请重新输入。");
                }

                var bizUser = model.ToUserModel();
                UserBusiness ub = new UserBusiness();
                ub.AddUser(bizUser);

                ModelState.AddModelError("", "新用户添加成功。");
                return View("New", new UserViewModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View("New", new UserViewModel());
            }
        }

        [HttpPost]
        [AcceptButton("btnReset")]
        public ActionResult Reset()
        {
            return View("New", new UserViewModel());
        }
    }
}
