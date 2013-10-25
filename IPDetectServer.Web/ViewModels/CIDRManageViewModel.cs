using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetectServer.Web.Models;
using IPDetectServer.Models;
using IPDetectServer.Repositories;

namespace IPDetectServer.Web.ViewModels
{
    public class CIDRManageViewModel : PagedModel<CIDRSettingModel>
    {
        public string ID
        {
            get;
            set;
        }

        public string IPStart
        {
            get;
            set;
        }

        public string IPEnd
        {
            get;
            set;
        }

        public int TCPPort
        {
            get;
            set;
        }

        public int TCPFaZhi
        {
            get;
            set;
        }

        public int TTLFaZhi
        {
            get;
            set;
        }

        public override void QueryData()
        {
            CIDRSettingRepository rep = new CIDRSettingRepository();

            this.DataList = rep.GetSettings( this.PageIndex, this.PageSize);
        }

        public override void CountTotalRows()
        {
            CIDRSettingRepository rep = new CIDRSettingRepository();
            this.TotalRows = rep.GetRowTotal();
        }
    }
}