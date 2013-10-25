using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IPDetectServer.Models
{
    [DataContract(Name = "CIDRSettingModel")]
    public class CIDRSettingModel
    {
        [DataMember(Name = "ID")]
        public string ID
        {
            get;
            set;
        }

        [DataMember(Name = "IPStart")]
        public string IPStart
        {
            get;
            set;
        }

        [DataMember(Name = "IPEnd")]
        public string IPEnd
        {
            get;
            set;
        }

        [DataMember(Name = "TCPPort")]
        public int TCPPort
        {
            get;
            set;
        }

        [DataMember(Name = "TCPFaZhi")]
        public int TCPFaZhi
        {
            get;
            set;
        }

        [DataMember(Name = "TTLFaZhi")]
        public int TTLFaZhi
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

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(IPStart))
            {
                return IPStart;
            }

            string[] ipSection = IPStart.Split('.');

            if (ipSection.Length != 4)
            {
                return IPStart;
            }

            ipSection[0] = ipSection[0].PadLeft(3, '0');
            ipSection[1] = ipSection[1].PadLeft(3, '0');
            ipSection[2] = ipSection[2].PadLeft(3, '0');
            ipSection[3] = ipSection[3].PadLeft(3, '0');

            return string.Format("{0}.{1}.{2}.{3}", ipSection[0], ipSection[1], ipSection[2], ipSection[3]);
        }
    }
}
