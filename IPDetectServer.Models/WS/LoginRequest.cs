using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace IPDetectServer.Models
{
    [DataContract(Name = "LoginRequest")]
    public class LoginRequest
    {
        [Display(Name = "UserName")]
        [DataMember(Name = "UserName", IsRequired = true)]
        public string UserName { get; set; }
      
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [DataMember(Name = "Password", IsRequired = true)]
        public string Password { get; set; }

        [DataMember(Name = "MacAddress")]
        public string MacAddress { get; set; }
    }
}