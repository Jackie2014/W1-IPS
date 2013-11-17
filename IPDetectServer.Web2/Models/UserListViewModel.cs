using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Models;
using IPDetectServer.Business;
using IPDetectServer.Repositories;

namespace IPDetectServer.Web.Models
{
    public class UserListViewModel : PagedModel<UserViewModel>
    {
        public string SelectedLoginName
        {
            get;
            set;
        }

        public string EditName
        {
            get;
            set;
        }

        public string EditCity
        {
            get;
            set;
        }

        public string EditDescription
        {
            get;
            set;
        }

        public string EditPhone
        {
            get;
            set;
        }

        public override void QueryData()
        {
            UserBusiness biz = new UserBusiness();
            List<UserModel> userList = biz.QueryUsers(this.PageIndex, this.PageSize);

            if (userList != null)
            {
                List<UserViewModel> dataList = new List<UserViewModel>();
                int index = PageIndex * PageSize;
                
                foreach (var item in userList)
                {
                    var viewModel = ConvertBizUserToViewModel(item);
                    viewModel.SeqNo = ++index;

                    dataList.Add(viewModel);
                }

                UpdateLoginDate(dataList);
                this.DataList = dataList;
            }
           
        }

        private void UpdateLoginDate(List<UserViewModel> users)
        {
            LoginRecordRepository lrr = new LoginRecordRepository();
            foreach (var item in users)
            {
                var loginRecord = lrr.GetLatestLoginRecord(item.LoginName);
                DateTime loginDate = DateTime.MinValue;

                if (loginRecord != null && loginRecord.LoginDate.HasValue)
                {
                    loginDate = loginRecord.LoginDate.Value;
                }

                item.LastLoginDate = loginDate;
            }
        }

        public override void CountTotalRows()
        {
            UserBusiness biz = new UserBusiness();
            List<UserModel> userList = biz.QueryUsers(0, int.MaxValue);

            this.TotalRows = userList.Count;
        }

        private UserViewModel ConvertBizUserToViewModel(UserModel userModel)
        {
            if (userModel == null)
                return null;

            var result = new UserViewModel
            {
                Address = userModel.Address,
                City = userModel.City,
                CreatedDate = userModel.CreatedDate,
                LoginName = userModel.LoginName,
                Name = userModel.Name,
                Phone = userModel.Phone,
                UserType = userModel.UserType,
                Description = userModel.Description,
            };

            return result;
        }
    }
}