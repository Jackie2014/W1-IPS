using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetectServer.Web
{
    public abstract class ListModelBase<T>
    {
        public IList<T> ListData { get; set; }

        public abstract int GetRecordCount(IDictionary<string, string> searchConditions);

        public virtual void GetRecord(IDictionary<string, string> searchConditions,
                              IDictionary<string, string> sortExpression,
                              int currentPage,
                              int pageSize)
        {
        }

        public virtual void GetRecord(IDictionary<string, string> searchConditions,
                      IDictionary<string, string> sortExpressione)
        {
        }

        public abstract void DeleteApplcations(IList<Guid> ids);
    }
}