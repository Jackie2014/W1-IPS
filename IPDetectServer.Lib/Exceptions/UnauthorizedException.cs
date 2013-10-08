using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
    public class UnauthorizedException : MobileException
    {
        // 401
        // client uses an not existing or invalid token, or not pass any credential
        public UnauthorizedException() 
        {}

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message,string detail)
            : base(message,detail)
        {
        }

        public UnauthorizedException(string message, string detail, string errorCode)
            : base(message, detail, errorCode)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnauthorizedException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
    }
}
