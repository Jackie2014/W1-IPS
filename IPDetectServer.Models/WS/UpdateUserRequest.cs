using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IPDetectServer.Models.WS
{
    [DataContract(Name = "UpdateUserRequest")]
    public class UpdateUserRequest
    {
        [DataMember(Name = "Name", IsRequired = true)]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "City")]
        public string City
        {
            get;
            set;
        }

        [DataMember(Name = "Phone")]
        public string Phone
        {
            get;
            set;
        }

        [DataMember(Name = "Description")]
        public string Description
        {
            get;
            set;
        }
    }
}
