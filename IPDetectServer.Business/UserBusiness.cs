using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPDetectServer.Repositories;
using IPDetectServer.Lib.Encrypt;
using IPDetectServer.Lib.Common;
using IPDetectServer.Models;
using IPDetectServer.Lib.Exceptions;

namespace IPDetectServer.Business
{
    public class UserBusiness
    {
        public List<UserModel> QueryUsers(int pageIndex, int pageSize)
        {
            var ur = new UserRepository();

            var dbUsers = ur.GetUsers(pageIndex,pageSize);

            var bizUsers = new List<UserModel>();
            if (dbUsers != null)
            {
                foreach (var dbUser in dbUsers)
                {
                    bizUsers.Add(ConvertDBUserToBizUser(dbUser));
                }
            }

            return bizUsers;
        }


        public void AddUser(UserModel user)
        {
            if (user == null || String.IsNullOrWhiteSpace(user.LoginName)
                || String.IsNullOrWhiteSpace(user.Password))
            {

            }
            UserRepository ur = new UserRepository();
            var u = ur.GetUser(user.LoginName);

            if (u != null)
            {
                throw new MobileException("该用户名已经存在,请重新输入用户名。", "", "user_has_exist");
            }

            user.Password = MD5Helper.GetMd5Hash(user.Password);
            var dbUser = ConvertBizUserToDbUser(user);
            dbUser = ur.AddUser(dbUser);
        }

        public UserModel GetUser(string loginName)
        {
            if(String.IsNullOrWhiteSpace(loginName))
            {
                return null;
            }
            UserRepository ur = new UserRepository();
            var u = ur.GetUser(loginName);
            var result = ConvertDBUserToBizUser(u);
            return result;
        }

        public bool Login(string userName, string password)
        {
            string md5Password = MD5Helper.GetMd5Hash(password);
            UserRepository ur = new UserRepository();
            User dbuser = ur.Login(userName, md5Password);

            return dbuser == null ? false : true;
        }

        public bool Login(string userName, string password,out UserModel user, bool isAdminLoginFromPortal = false,string clientIP = null,string macAddr = null)
        {
            user = null;
            bool isLogin = false;
            string md5Password = isAdminLoginFromPortal ? MD5Helper.GetMd5Hash(password) : password;
            UserRepository ur = new UserRepository();
            User dbuser = ur.Login(userName, md5Password);

            if (dbuser != null)
            {
                user = ConvertDBUserToBizUser(dbuser);
                // 客户端用户不能登录后台管理系统
                if (isAdminLoginFromPortal && (int)RoleType.ClientUsers == user.UserType)
                {
                    isLogin = false;
                }
                else
                {
                    isLogin = true;

                    TokenRepository tr = new TokenRepository();
                    var tokenModel = new token
                    {
                        IP = clientIP,
                        MacAddr = macAddr,
                        UserName = userName
                    };

                    user.Token = tr.NewToken(tokenModel);
                    // login success, add login record
                    LoginRecordRepository recordRep = new LoginRecordRepository();
                    LoginRecord loginRecord = new LoginRecord
                    {
                        IsLoginFromClient = isAdminLoginFromPortal ? 0 : 1,
                        LoginIP = clientIP,
                        UserName = userName
                    };

                    recordRep.Add(loginRecord);
                }
            }

            return isLogin;
        }

        public void DeleteUser(string userId)
        {
            UserRepository ur = new UserRepository();
            ur.DeleteUser(userId);
        }

        public void ChangePassword(string userName, string oldPassword, string newPassword, bool doingManage = false)
        {
            UserRepository ur = new UserRepository();

            if (!doingManage)
            {
                string md5Password = MD5Helper.GetMd5Hash(oldPassword);
                
                User dbuser = ur.Login(userName, md5Password);
                if (dbuser == null)
                {
                    throw new Exception("当前密码不正确！");
                }
            }

            string md5NewPassword = MD5Helper.GetMd5Hash(newPassword);
            ur.ChangePassword(userName, md5NewPassword);
        }

        private User ConvertBizUserToDbUser(UserModel user)
        {
            User dbUser = new User()
            {
                UserName = user.LoginName,
                FullName = user.Name,
                UserType = user.UserType,
                Address = user.Address,
                City = user.City,
                Province = user.Province,
                Password = user.Password,
                Phone = user.Phone,
                Email = user.Email,
                Description = user.Description,
                CreatedBy = user.CreatedBy,
                LastUpdatedBy = user.LastUpdatedBy
            };

            return dbUser;
        }

        private UserModel ConvertDBUserToBizUser(User dbUser)
        {
            UserModel user = new UserModel()
            {
                LoginName = dbUser.UserName,
                Name = dbUser.FullName,
                UserType = dbUser.UserType,
                Address = dbUser.Address,
                City = dbUser.City,
                Province = dbUser.Province,
                Password = dbUser.Password,
                Phone = dbUser.Phone,
                Email = dbUser.Email,
                Description = dbUser.Description,
                CreatedBy = dbUser.CreatedBy,
                CreatedDate = dbUser.CreatedDate.HasValue? dbUser.CreatedDate.Value : DateTime.MinValue,
                LastUpdatedBy = dbUser.LastUpdatedBy,
                LastUpdatedDate = dbUser.LastUpdatedDate.HasValue ? dbUser.LastUpdatedDate.Value : DateTime.MinValue
            };

            return user;
        }
    }
}
