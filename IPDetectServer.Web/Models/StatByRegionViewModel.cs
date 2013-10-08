using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Models;
using IPDetectServer.Business;
using System.Configuration;

namespace IPDetectServer.Web.Models
{
    public class StatByRegionViewModel : PagedModel<GroupByRegionModel>
    {
        //public string Region
        //{
        //    get;
        //    set;
        //}

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
            List<GroupByRegionModel> groupList = biz.GroupByRegion(CurrentProvince, Start, End, 0, int.MaxValue);
            this.DataList = groupList;
        }

        public override void CountTotalRows()
        {
            //IPQueryBiz biz = new IPQueryBiz();
            //List<GroupByRegionModel> groupList = biz.GroupByRegion(CurrentProvince, Start, End, 0, int.MaxValue);

            //this.TotalRows = groupList.Count;
        }

        private string CurrentProvince
        {
            get
            {
                return ConfigurationManager.AppSettings["CurrentProvince"];
            }
        }
    }
}