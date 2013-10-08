using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetectServer.Web.Models
{
    public class SearchConditionDictionary : Dictionary<string,string>
    {
        public new string this[string key]
        {
            get{
                if (!base.ContainsKey(key))
                {
                    base.Add(key, string.Empty);
                }
                return base[key];
            }
            set{
                if (!base.ContainsKey(key))
                {
                    base.Add(key, string.Empty);
                }

                base[key] = value;
            }
        }
    }
}