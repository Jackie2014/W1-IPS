using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IPDetectServer.Models
{
    [DataContract(Name = "IPScanResult")]
    public class IPScanResult
    {
        public int Seq
        {
            get;
            set;
        }

        [DataMember(Name = "IP")]
        public string IP
        {
            get;
            set;
        }

        [DataMember(Name = "TCPTime")]
        public int TCPResponseTime
        {
            get;
            set;
        }

        [DataMember(Name = "TCPResult")]
        public string TCPValidationResult
        {
            get;
            set;
        }

        [DataMember(Name = "TTLResult")]
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
