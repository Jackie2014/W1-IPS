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

namespace IPDetectServer.Services
{
    [RoutePrefix("apis/v1/token")]
    public class TokenController : ApiController
    {
        [POST("")]
        public LoginResponse Login([FromBody]LoginRequest user)
        {
            if (user == null)
            {
                throw new BadRequestException();
            }

            var response = new LoginResponse();
            
            UserBusiness ub = new UserBusiness();
            UserModel userModel = null;
            bool isLogin = ub.Login(user.UserName, user.Password, out userModel, false, Context.ClientIP);
            
            if (!isLogin)
            {
                throw new UnauthorizedException("非法的用户名或密码!");
            }
            else
            {
                response.Token = userModel.Token;

                // add login record
                //LoginRecordRepository recordRep = new LoginRecordRepository();
                //LoginRecord loginRecord = new LoginRecord 
                //{ 
                //    IsLoginFromClient = 1,
                //    LoginIP = Context.ClientIP,
                //    UserName = user.UserName
                //};
                //recordRep.Add(loginRecord);
            }

            return response;
        }
    }
}
