using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace IPDetectServer.Models
{
    public class ClientIPModel
    {
        public int ID
        {
            get;
            set;
        }

        public string ClientIP
        {
            get;
            set;
        }

        public string ClientPrivateIP
        {
            get;
            set;
        }

        public string ClientRecordor
        {
            get;
            set;
        }

        public string ClientProvince
        {
            get;
            set;
        }

        public string ClientCity
        {
            get;
            set;
        }
        public string ClientDistinct
        {
            get;
            set;
        }
        public string ClientAddress
        {
            get;
            set;
        }

        public string ClientDetailAddress
        {
            get
            {
                return String.Format("{0}{1}{2}{3}", ClientProvince, ClientCity, ClientDistinct, ClientAddress);
            }
        }

        public string ExpectedISP
        {
            get;
            set;
        }

        public string ExpectedISPProvince
        {
            get;
            set;
        }

        public string ExpectedISPCity
        {
            get;
            set;
        }

        public string RealISP
        {
            get;
            set;
        }

        public string RealISPProvince
        {
            get;
            set;
        }

        public string RealISPCity
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }
        public DateTime? CreatedDate
        {
            get;
            set;
        }

        public IPMonitorStatus Status
        {
            get;
            set;
        }

        public string StatusForDisplay
        {
            get
            {
                string display = "未诊断";

                switch (Status)
                {
                    case IPMonitorStatus.Pending:
                        display = "未诊断";
                        break;
                    case IPMonitorStatus.Normal:
                        display = "正常";
                        break;
                    case IPMonitorStatus.Exception:
                        display = "异常";
                        break;
                    case IPMonitorStatus.Unknown:
                        display = "无法判断";
                        break;
                }

                return display;
            }
        }
    }
}
