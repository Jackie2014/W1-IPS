using AttributeRouting;
using AttributeRouting.Web.Http;
using IPDetectServer.Lib.Exceptions;
using IPDetectServer.Lib;
using IPDetectServer.Lib.Encrypt;
using IPDetectServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using IPDetectServer.Business;
using IPDetectServer.Models;
using IPDetectServer.Models.WS;

namespace IPDetectServer.Services
{
    [RoutePrefix("apis/v1/users")]
    public class UserController : ApiController
    {
        [POST("{userid}/changepassword")]
        public ChangePasswordResponse Login([FromBody]ChangePasswordRequest request,string userid)
        {
            if (request == null)
            {
                throw new BadRequestException();
            }

            var response = new ChangePasswordResponse();
            UserBusiness ub = new UserBusiness();

            try
            {
                if ("Administrator".Equals(userid, StringComparison.OrdinalIgnoreCase))
                {
                    response.IsSuccess = false;
                    response.Message = "不能修改Administrator密码。";
                    return response;
                }

                var user = ub.GetUser(userid);

                // 2 means 客户端用户
                if (user.UserType == 2)
                {
                    response.IsSuccess = false;
                    response.Message = "终端用户没有权限修改密码。";
                    return response;
                }

                ub.ChangePassword(userid, request.OldPassword, request.NewPassword,true);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [POST("{userid}/update")]
        public AjaxResponse Update([FromBody]UpdateUserRequest request, string userid)
        {
            if (request == null)
            {
                throw new BadRequestException();
            }

            var response = new AjaxResponse();
            UserBusiness ub = new UserBusiness();

            try
            {
                if ("Administrator".Equals(userid, StringComparison.OrdinalIgnoreCase))
                {
                    response.IsSuccess = false;
                    response.Message = "不能修改Administrator密码。";
                    return response;
                }
                var user = ub.GetUser(userid);

                // 2 means 客户端用户
                if (user.UserType == 2)
                {
                    response.IsSuccess = false;
                    response.Message = "终端用户没有权限修改密码。";
                    return response;
                }

                UserRepository ur = new UserRepository();
                User dbUser = new Repositories.User();
                dbUser.UserName = userid;
                dbUser.FullName = request.Name;
                dbUser.Phone = request.Phone;
                dbUser.City = request.City;
                dbUser.Description = request.Description;

                ur.UpdateUser(dbUser);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [DELETE("{userid}")]
        public AjaxResponse DeleteUser(string userid)
        {
            var response = new AjaxResponse();

            try
            {
                if ("Administrator".Equals(userid, StringComparison.OrdinalIgnoreCase))
                {
                    response.IsSuccess = false;
                    response.Message = "Administrator账号不能被删除。";
                    return response;
                }

                UserBusiness ub = new UserBusiness();

                var user = ub.GetUser(Context.LoginName);

                // 2 means 客户端用户
                if (user.UserType == 2)
                {
                    response.IsSuccess = false;
                    response.Message = "终端用户没有权限进行此操作。";
                    return response;
                }

                ub.DeleteUser(userid);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
