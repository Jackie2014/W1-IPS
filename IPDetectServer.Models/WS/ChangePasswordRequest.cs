using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IPDetectServer.Models.WS
{
    [DataContract(Name = "ChangePasswordRequest")]
    public class ChangePasswordRequest
    {
        [DataMember(Name = "OldPassword", IsRequired = true)]
        public string OldPassword
        {
            get;
            set;
        }

        [DataMember(Name = "NewPassword", IsRequired = true)]
        public string NewPassword
        {
            get;
            set;
        }
    }
}
