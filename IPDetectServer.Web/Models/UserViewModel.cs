using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Models;

namespace IPDetectServer.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        { }

        public UserViewModel(UserModel user)
        {
            if (user != null)
            {
                LoginName = user.LoginName;
                UserType = user.UserType;
                Name = user.Name;
                Phone = user.Phone;
                Description = user.Description;
                City = user.City;
            }
        }

        public int SeqNo
        {
            get;
            set;
        }

        public string LoginName
        {
            get;
            set;
        }

        public int UserType
        {
            get;
            set;
        }
        public string UserTypeForDisplay
        {
            get
            {
                switch (UserType)
                {
                    case 0:
                        return "超级管理员";
                    case 1:
                        return "后台管理员";
                    case 2:
                        return "客户端用户";
                    default:
                        return "无效的类型";
                }
            }
        }
        public List<SelectListItem> GetUserTypes()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "客户端用户", Value = "2", });
            types.Add(new SelectListItem { Text = "后台管理员", Value = "1" });
            return types;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string PasswordConfirm
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Province
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public DateTime LastLoginDate
        {
            get;
            set;
        }

        public UserModel ToUserModel()
        {
            UserModel u = new UserModel()
            {
                LoginName = this.LoginName,
                Name = this.Name,
                Password = this.Password,
                City = this.City,
                Phone = this.Phone,
                Description = this.Description,
                UserType = this.UserType
            };

            return u;
        }
    }
}