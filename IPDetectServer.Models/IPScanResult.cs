using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models
{
    public class IPScanResult
    {
        public string IP
        {
            get;
            set;
        }

        public int TCPResponseTime
        {
            get;
            set;
        }

        public string TCPValidationResult
        {
            get;
            set;
        }

        public string TTLValidationResult
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public string LastUpdatedBy
        {
            get;
            set;
        }

        public DateTime LastUpdatedDate
        {
            get;
            set;
        }
    }
}
