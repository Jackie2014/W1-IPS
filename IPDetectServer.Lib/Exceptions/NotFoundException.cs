using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
    public class NotFoundException : MobileException
    {
        //404
        public NotFoundException() 
        {}

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message,string detail)
            : base(message,detail)
        {
        }

        public NotFoundException(string message, string detail, string errorCode)
            : base(message, detail, errorCode)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
    }
}
