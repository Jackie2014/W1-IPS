using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IPDetectServer.Web.Models;

 namespace IPDetectServer.Web
{
    /// <summary>
    /// Grid controller base class.
    /// </summary>
    public abstract class ListControllerBase : CustomizedControllerBase
    {
        private ViewStateManager _viewStateManager;

        public int CurrentPage
        {
            get
            {
                return ViewBag.CurrentPage;
            }
            set
            {
                ViewBag.CurrentPage = value;
                
            }
        }

        public int TotalRecords
        {
            get
            {
                return ViewBag.TotalRecords;
            }
            set
            {
                ViewBag.TotalRecords = value;
            }
        }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Sort condtions.
        /// </summary>
        public IDictionary<string, string> SortConditions { get; set; }

        /// <summary>
        /// Search conditions.
        /// </summary>
        public IDictionary<string, string> SearchConditions { get; set; }


        public ListControllerBase()
        {
 
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.InitData();
            //this.GetMultiLangLabelValue();
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            _viewStateManager = new ViewStateManager(requestContext.HttpContext.Request, ViewBag, this.GetViewStateFieldName());
            LoadViewState(_viewStateManager);

            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            _viewStateManager = new ViewStateManager(requestContext.HttpContext.Request, ViewBag, this.GetViewStateFieldName());
            LoadViewState(_viewStateManager);

            base.Execute(requestContext);
        }

        protected override ViewResult View(IView view, object model)
        {
            SaveViewState(_viewStateManager);
            return base.View(view, model);
        }
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            SaveViewState(_viewStateManager);
            return base.View(viewName, masterName, model);
        }

        protected virtual string GetViewStateFieldName()
        {
            return "_ViewState";
        }

        protected virtual void InitData()
        {
            //var tem_pageSize = ConfigHelper.GetPageSize();
            ViewBag.PageSize = 20;

            //if (tem_pageSize != null)
            //{
            //    ViewBag.PageSize = tem_pageSize.Value;
            //}

            this.PageSize = ViewBag.PageSize;
        }

        protected virtual void LoadViewState(ViewStateManager viewStateManager)
        {
            this.CurrentPage = viewStateManager.GetViewState<int>("CurrentPage");
            this.TotalRecords = viewStateManager.GetViewState<int>("TotalRecords");
            this.SearchConditions = viewStateManager.GetViewState<IDictionary<string, string>>("SearchConditions");
            this.SortConditions = viewStateManager.GetViewState<IDictionary<string, string>>("SortConditions");
            if (this.SearchConditions == null)
            {
                this.SearchConditions = new Dictionary<string, string>();
            }
            if (this.SortConditions == null)
            {
                this.SortConditions = new Dictionary<string, string>();
            }
        }

        protected virtual void SaveViewState(ViewStateManager viewStateManager)
        {
            _viewStateManager.SetViewState("CurrentPage", this.CurrentPage);
            _viewStateManager.SetViewState("TotalRecords", this.TotalRecords);
            _viewStateManager.SetViewState("SearchConditions", this.SearchConditions);
            _viewStateManager.SetViewState("SortConditions", this.SortConditions);
        }


        /// <summary>
        /// Get multi language label value.
        /// </summary>
        //protected abstract void GetMultiLangLabelValue();

        /// <summary>
        /// Get selected item's key list.
        /// </summary>
        /// <returns>Key list.</returns>
        protected IList<Guid> GetSelectedItemKeys()
        {
            var selectedItemKeys = new List<Guid>();
            string IdString = Request["hidGridSelectedIds"];
            if (!string.IsNullOrWhiteSpace(IdString))
            {
                var Ids = IdString.Split(',');
                foreach (string id in Ids)
                {
                    selectedItemKeys.Add(new Guid(id));
                }
            }

            return selectedItemKeys;
        }      

        /// <summary>
        /// Set search condition.
        /// </summary>
        //protected abstract void CollectSearchConditionFromUI();

        /// <summary>
        /// Set paging parameter.
        /// </summary>
        //protected void CollectCurrentPageInfoFromUI(PagedModel<T> model )
        //{
        //    string pageIndex = Request["txtPageNaviNo"];
        //    if (!string.IsNullOrWhiteSpace(pageIndex))
        //    {
        //        this.CurrentPage = int.Parse(pageIndex);
        //    }
        //    else
        //    {
        //        this.CurrentPage = 0;
        //    }
        //}

        protected void CollectSortConditionFromUI()
        {
            this.SortConditions.Clear();
            string sortName = Request.Form["hidSortFieldName"];
            string sortValue = Request.Form["hidSortFieldOrderBy"];
            if (!string.IsNullOrWhiteSpace(sortName))
            {
                if (string.IsNullOrWhiteSpace(sortName))
                {
                    sortValue = "ASC";
                }

                this.SortConditions.Add(sortName, sortValue);
            }
        }

        /// <summary>
        /// quickly set search condition
        /// </summary>
        /// <param name="key">condition key</param>
        /// <param name="value">condition value</param>
        protected void SetCondition(string key, string value)
        {
            if (this.SearchConditions.ContainsKey(key))
            {
                this.SearchConditions[key] = value;
            }
            else
            {
                this.SearchConditions.Add(key, value);
            }
        }
    }
}