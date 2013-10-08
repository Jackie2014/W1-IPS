using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Lib.Exceptions
{
    public class DuplicateObjectException : MobileException
    {
        //400
        public DuplicateObjectException() 
        {}

        public DuplicateObjectException(string message) : base(message)
        {
        }

        public DuplicateObjectException(string message,string detail)
            : base(message,detail)
        {
        }

        public DuplicateObjectException(string message, string detail, string errorCode)
            : base(message, detail, errorCode)
        {
        }

        public DuplicateObjectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DuplicateObjectException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
    }
}
