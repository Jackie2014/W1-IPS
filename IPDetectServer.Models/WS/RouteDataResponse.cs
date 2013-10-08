using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models.WS
{
    public class RouteDataResponseItem
    {
        public int ParentUID
        {
            get;
            set;
        }

        public int SeqNo
        {
            get;
            set;
        }

        public string RouteIP
        {
            get;
            set;
        }

        public string IPBelongTo
        {
            get;
            set;
        }

        public string IPBelongToProvince
        {
            get;
            set;
        }

        public string IPBelongToCity
        {
            get;
            set;
        }
    }
}
