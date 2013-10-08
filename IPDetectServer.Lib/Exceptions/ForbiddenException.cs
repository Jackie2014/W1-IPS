using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
    public class ForbiddenException : MobileException
    {
        // 403 - expired token or no permission to access this resource
        public ForbiddenException() 
        {}

        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string message,string detail)
            : base(message,detail)
        {
        }

        public ForbiddenException(string message, string detail, string errorCode)
            : base(message, detail, errorCode)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ForbiddenException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
    }
}
