using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace IPDetectServer.Models
{
    [DataContract(Name = "MonitorDataRequest")]
    public class MonitorDataRequest
    {
        /// <summary>
        /// Only exist data when data submited from child server
        /// </summary>
        [DataMember(Name = "ClientPublicIP")]
        public string ClientPublicIP { get; set; }

        [DataMember(Name = "ClientPrivateIP")]
        public string ClientPrivateIP { get; set; }

        /// <summary>
        /// Only exist data when data submited from child server
        /// </summary>
        [DataMember(Name = "SubmitFromServerIP")]
        public string SubmitFromServerIP { get; set; }

        [DataMember(Name = "ClientProvince")]
        public string ClientProvince { get; set; }

        [DataMember(Name = "ClientCity")]
        public string ClientCity { get; set; }

        [DataMember(Name = "ClientDistinct")]
        public string ClientDistinct { get; set; }

        [DataMember(Name = "ClientDetailAddress")]
        public string ClientDetailAddress { get; set; }

        [DataMember(Name = "ExpectedOperator")]
        public string ExpectedOperator { get; set; }

        [DataMember(Name = "ExpectedOperatorProvice")]
        public string ExpectedOperatorProvice { get; set; }

        [DataMember(Name = "ExpectedOperatorCity")]
        public string ExpectedOperatorCity { get; set; }

        [DataMember(Name = "ClientRecordor")]
        public string ClientRecordor { get; set; }
    }
}