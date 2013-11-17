using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Models;
using IPDetectServer.Business;

namespace IPDetectServer.Web.Models
{
    public class StatByAccountViewModel : PagedModel<GroupByAccountModel>
    {
        public string UserName
        {
            get;
            set;
        }

        public bool SelectedAllAccounts
        {
            get;
            set;
        }

        public string SelectedAccount
        {
            get;
            set;
        }

        public DateTime Start
        {
            get;
            set;
        }

        public DateTime End
        {
            get;
            set;
        }

        public override void QueryData()
        {
            IPQueryBiz biz = new IPQueryBiz();
            List<GroupByAccountModel> groupList = biz.GroupByAccount(UserName, Start,End,this.PageIndex, this.PageSize);
            this.DataList = groupList;
        }

        public override void CountTotalRows()
        {
            IPQueryBiz biz = new IPQueryBiz();
            List<GroupByAccountModel> groupList = biz.GroupByAccount(UserName, Start, End, 0, int.MaxValue);

            this.TotalRows = groupList.Count;
        }
    }
}