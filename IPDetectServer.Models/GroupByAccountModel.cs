using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models
{
    public class GroupByAccountModel
    {
        public string UserName
        {
            get;
            set;
        }

        public int StatusNormalCount
        {
            get;
            set;
        }

        public int StatusUnknownCount
        {
            get;
            set;
        }

        public int StatusExceptionCount
        {
            get;
            set;
        }

        public int Total
        {
            get
            {
                return StatusNormalCount + StatusUnknownCount + StatusExceptionCount;
            }
        }
    }
}
