using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Models.WS
{
    //public class RouteDataRequest
    //{
    //    public List<RouteDataRequestItem> Items
    //    {
    //        get;
    //        set;
    //    }
    //}

    public class RouteDataRequestItem
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

        public string T1
        {
            get;
            set;
        }

        public string T2
        {
            get;
            set;
        }

        public string T3
        {
            get;
            set;
        }

        public string RouteIP
        {
            get;
            set;
        }

        //public string IPBelongTo
        //{
        //    get;
        //    set;
        //}

        public DateTime RouteDate
        {
            get;
            set;
        }
    }
}
