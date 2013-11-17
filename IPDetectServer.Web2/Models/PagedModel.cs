using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace IPDetectServer.Web.Models
{
    public abstract class PagedModel<T>
    {
        public virtual int TotalRows
        {
            get;
            set;
        }

        private const int DEFAULT_PAGE_SIZE = 15;
        private int _pageSize = DEFAULT_PAGE_SIZE;
        public virtual int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    _pageSize = DEFAULT_PAGE_SIZE;
                }

                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        private const int DEFAULT_PAGE_INDEX = 0;
        private int _pageIndex = 0;
        public virtual int PageIndex
        {
            get
            {
                if (_pageIndex < 0)
                {
                    _pageIndex = DEFAULT_PAGE_INDEX;
                }

                return _pageIndex;
            }
            set
            {
               _pageIndex = value;
            }
        }

        public virtual List<T> DataList
        {
            get;
            set;
        }

        private SearchConditionDictionary _searchConditions = null;
        public virtual SearchConditionDictionary SearchConditions
        {
            get
            {
                if (_searchConditions == null)
                {
                    _searchConditions = new SearchConditionDictionary();
                }
                return _searchConditions;
            }
            set
            {
                _searchConditions = value;
            }
        }

        private SearchConditionDictionary _sortConditions = null;
        public virtual SearchConditionDictionary SortConditions
        {
            get
            {
                if (_sortConditions == null)
                {
                    _sortConditions = new SearchConditionDictionary();
                }
                return _sortConditions;
            }
            set
            {
                _sortConditions = value;
            }
        }

        public virtual void CountTotalRows()
        {

        }

        public abstract void QueryData();
    }
}