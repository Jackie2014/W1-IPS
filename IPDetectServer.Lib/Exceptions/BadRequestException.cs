using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
    public class BadRequestException : MobileException
    {
        //400
        public BadRequestException() 
        {}

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message,string detail)
            : base(message,detail)
        {
        }

        public BadRequestException(string message, string detail, string errorCode)
            : base(message, detail, errorCode)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BadRequestException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
    }
}
