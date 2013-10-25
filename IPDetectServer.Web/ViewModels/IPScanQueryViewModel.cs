using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Web.Models;
using IPDetectServer.Models;
using IPDetectServer.Repositories;
using IPDetectServer.Lib.Common;

namespace IPDetectServer.Web.ViewModels
{
    public class IPScanQueryViewModel : PagedModel<IPScanResult>
    {
        private DateTime Start
        {
            get
            {
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

        public string TCPStatus
        {
            get;
            set;
        }

        public string TTLStatus
        {
            get;
            set;
        }

        public int TCPOk
        {
            get;
            set;
        }

        public int TCPException
        {
            get;
            set;
        }

        public int TTLOK
        {
            get;
            set;
        }

        public int TTLException
        {
            get;
            set;
        }

        public List<SelectListItem> GetStatusList()
        {
            List<SelectListItem> statuses = new List<SelectListItem>();
            statuses.Add(new SelectListItem { Text = "全部", Value = "", });
            statuses.Add(new SelectListItem { Text = "异常", Value = "异常" });
            statuses.Add(new SelectListItem { Text = "正常", Value = "正常" });

            return statuses;
        }

        public override void QueryData()
        {
            IPScanResultRepository biz = new IPScanResultRepository();

            this.DataList = biz.QueryIPScanResults(Start, End.AddDays(1),this.TCPStatus,this.TTLStatus, this.PageIndex, this.PageSize);
        }

        public override void CountTotalRows()
        {
            IPScanResultRepository biz = new IPScanResultRepository();
            this.TotalRows = biz.GetRowTotal(Start, End.AddDays(1), this.TCPStatus, this.TTLStatus);
        }
    }
}