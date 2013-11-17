using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Repositories;
using IPDetectServer.Models;
using IPDetectServer.Business;
using System.Web.Mvc;
using IPDetectServer.Lib.Common;

namespace IPDetectServer.Web.Models
{
    public class IPQueryModel : PagedModel<ClientIPModel>
    {
        public IPQueryModel()
        {
            Status = -1;
        }

        public string SelectedUserID
        {
            get;
            set;
        }

        public string SelectedRegion
        {
            get;
            set;
        }

        private DateTime Start
        {
            get {
                if (String.IsNullOrWhiteSpace(this.SearchConditions["StartDate"]))
                {
                    return Constants.MinDate;
                }
                else
                {
                    DateTime result = Constants.MinDate;
                    bool isSuccess = DateTime.TryParse(this.SearchConditions["StartDate"], out result);
                    if (!isSuccess)
                    {
                        result = Constants.MinDate;
                        this.SearchConditions["StartDate"] = result.ToString("yyyy-MM-dd");
                    }
                    return result;
                }
            }
        }

        private DateTime End
        {
            get
            {
                if (String.IsNullOrWhiteSpace(this.SearchConditions["EndDate"]))
                {
                    return DateTime.Now; ;
                }
                else
                {
                    DateTime result = DateTime.Now;
                    bool isSuccess = DateTime.TryParse(this.SearchConditions["EndDate"], out result);
                    if (!isSuccess)
                    {
                        result = DateTime.Now;
                        this.SearchConditions["EndDate"] = result.ToString("yyyy-MM-dd");
                    }
                    return result;
                }
            }
        }

        public int Status
        {
            get;set;
        }

        public List<SelectListItem> GetStatusList()
        {
            List<SelectListItem> statuses = new List<SelectListItem>();
            statuses.Add(new SelectListItem { Text = "全部", Value = "-1",});
            statuses.Add(new SelectListItem { Text = "未诊断", Value = "0" });
            statuses.Add(new SelectListItem { Text = "正常", Value = "1" });
            statuses.Add(new SelectListItem { Text = "无法判断", Value = "2" });
            statuses.Add(new SelectListItem { Text = "异常", Value = "3" });

            return statuses;
        }

        public override void QueryData()
        {
            IPQueryBiz biz = new IPQueryBiz();

            this.DataList = biz.Query(SelectedRegion, SelectedUserID, Start, End.AddDays(1), this.PageIndex, this.PageSize, Status);
        }

        public override void CountTotalRows()
        {
            IPQueryBiz biz = new IPQueryBiz();
            this.TotalRows = biz.CountTotalRows(SelectedRegion,SelectedUserID, Start, End.AddDays(1), Status);
        }
    }
}