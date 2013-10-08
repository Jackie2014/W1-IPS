using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models
{
    public class GroupByRegionModel
    {
        public string Region
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
