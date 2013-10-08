using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Net;

namespace IPDetectServer.Lib.WSModels
{
    /// <summary>
    /// ResponseStatus
    /// </summary>
    [DataContract(Name = "error")]
    public class WSErrorResponse
    {
        public WSErrorResponse()
        {}

        public WSErrorResponse (HttpStatusCode status, string code, string message, string more)
        {
            Status = (int)status;
            Message = message;
            Code = code;
            More = more;
        }

        /// <summary>
        /// Http Status. one of HttpStatusCode enum.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// Http Status. one of HttpStatusCode enum.
        /// </summary>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Http Status. one of HttpStatusCode enum.
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// Http Status. one of HttpStatusCode enum.
        /// </summary>
        [DataMember(Name = "more", EmitDefaultValue = false)]
        public string More
        {
            get;
            set;
        }
    }
}
