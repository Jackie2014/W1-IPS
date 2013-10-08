using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Repositories
{
    public class GroupByRegionAndStatusModel
    {
        public string Region
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        public long Status
        {
            get;
            set;
        }
    }
}
