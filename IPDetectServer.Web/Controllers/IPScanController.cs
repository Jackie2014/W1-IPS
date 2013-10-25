using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Web.Models;
using IPDetectServer.Web.ViewModels;

namespace IPDetectServer.Web.Controllers
{
    public class IPScanController : CustomizedControllerBase
    {

        public ActionResult Index()
        {
            IPScanQueryViewModel model = InitilizeModel();
            return View(model);
        }

        public ActionResult Manage()
        {
            CIDRManageViewModel model = new CIDRManageViewModel();
            model.QueryData();
            return View("Manage", model);
        }

        public ActionResult Edit()
        {
            CIDRManageViewModel model = null;
            return View("Edit", model);
        }

        private IPScanQueryViewModel InitilizeModel()
        {
            IPScanQueryViewModel model = new IPScanQueryViewModel();
            model.SearchConditions["StartDate"] = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            model.SearchConditions["EndDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            return model;
        }

        [HttpPost]
        [AcceptButton("btnIPScanResultQuery")]
        public ActionResult SearchAction(IPScanQueryViewModel model)
        {
            try
            {
                return this.RefreshData(true, model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        [AcceptButton("btnSortInGrid")]
        public ActionResult SortAction(IPScanQueryViewModel model)
        {
            try
            {
                //this.CollectSortConditionFromUI();
                //model.SortConditions.Clear();
                string sortName = Request.Form["hidSortFieldName"];
                string sortValue = Request.Form["hidSortFieldOrderBy"];
                if (!string.IsNullOrWhiteSpace(sortName))
                {
                    if (string.IsNullOrWhiteSpace(sortName))
                    {
                        sortValue = "ASC";
                    }

                    model.SortConditions.Add(sortName, sortValue);
                }
                return this.RefreshData(true, model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        [AcceptButton("btnPageNavigate")]
        public ActionResult PagingAction(IPScanQueryViewModel model)
        {
            try
            {
                string pageIndex = Request.Form["txtPageNaviNo"];
                if (!string.IsNullOrWhiteSpace(pageIndex))
                {
                    model.PageIndex = int.Parse(pageIndex);
                }
                else
                {
                    model.PageIndex = 0;
                }
                //this.CollectCurrentPageInfoFromUI(model);
                return this.RefreshData(false, model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        protected ViewResult RefreshData(bool needReFetchRecordCount, IPScanQueryViewModel model)
        {
            if (needReFetchRecordCount)
            {
                model.CountTotalRows();
                ModelState.Remove("TotalRows");
            }
            
            model.QueryData();
            return View(model);
        }

        public IEnumerable<SelectListItem> GetStatusList()
        {
            List<SelectListItem> statuses = new List<SelectListItem>();
            statuses.Add(new SelectListItem { Text = "全部", Value = "-1", Selected = true });
            statuses.Add(new SelectListItem { Text = "未诊断", Value = "0" });
            statuses.Add(new SelectListItem { Text = "正常", Value = "1" });
            statuses.Add(new SelectListItem { Text = "无法识别", Value = "2" });
            statuses.Add(new SelectListItem { Text = "异常", Value = "3" });

            return statuses;
        }

        #region CIDR setting list

        [HttpPost]
        [AcceptButton("btnSortInGrid_CIDR")]
        public ActionResult SortAction_CIDR(CIDRManageViewModel model)
        {
            try
            {
                string sortName = Request.Form["hidSortFieldName"];
                string sortValue = Request.Form["hidSortFieldOrderBy"];
                if (!string.IsNullOrWhiteSpace(sortName))
                {
                    if (string.IsNullOrWhiteSpace(sortName))
                    {
                        sortValue = "ASC";
                    }

                    model.SortConditions.Add(sortName, sortValue);
                }
                return this.RefreshData_CIDR(true, model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        [AcceptButton("btnPageNavigate_CIDR")]
        public ActionResult PagingAction_CIDR(CIDRManageViewModel model)
        {
            try
            {
                string pageIndex = Request.Form["txtPageNaviNo"];
                if (!string.IsNullOrWhiteSpace(pageIndex))
                {
                    model.PageIndex = int.Parse(pageIndex);
                }
                else
                {
                    model.PageIndex = 0;
                }
                //this.CollectCurrentPageInfoFromUI(model);
                return this.RefreshData_CIDR(false, model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        protected ViewResult RefreshData_CIDR(bool needReFetchRecordCount, CIDRManageViewModel model)
        {
            if (needReFetchRecordCount)
            {
                model.CountTotalRows();
                ModelState.Remove("TotalRows");
            }

            model.QueryData();
            return View(model);
        }
        #endregion
    }
}
