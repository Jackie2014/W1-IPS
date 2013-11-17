using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Web.Models;
using IPDetectServer.Business;
using IPDetectServer.Lib.Common;

namespace IPDetectServer.Web.Controllers
{
    public class StatByAccountController : CustomizedControllerBase
    {
        //
        // GET: /StatByAccount/

        public ActionResult Index(StatByAccountViewModel model)
        {
            model.SelectedAllAccounts = true;
            model.SearchConditions["StartDate"] = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            model.SearchConditions["EndDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View(model);
        }

        public ActionResult OneAccount(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return View("index", new StatByAccountViewModel());
            }

            IPQueryModel model = new IPQueryModel();
            model.SelectedUserID = id;

            model.QueryData();
            model.CountTotalRows();
            return View(model);
        }

        [HttpPost]
        [AcceptButton("btnPageNavigate")]
        public ActionResult PagingAction(IPQueryModel model)
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
                model.SelectedUserID = Request.Form["hidUserID"];
                model.QueryData();
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        [AcceptButton("btnStatByAccout")]
        public ActionResult SearchAction(StatByAccountViewModel model)
        {
            try
            {
                model.UserName = "";
                if(model.SelectedAllAccounts == false)
                {
                    model.UserName = model.SelectedAccount;
                }

                if (String.IsNullOrWhiteSpace(model.SearchConditions["StartDate"]))
                {
                    model.Start = Constants.MinDate;
                }
                else
                {
                    DateTime result = Constants.MinDate;
                    bool isSuccess = DateTime.TryParse(model.SearchConditions["StartDate"], out result);
                    model.Start = isSuccess ? result : Constants.MinDate;
                }

                if (String.IsNullOrWhiteSpace(model.SearchConditions["EndDate"]))
                {
                    model.End = DateTime.MaxValue;
                }
                else
                {
                    DateTime result = DateTime.MaxValue;
                    bool isSuccess = DateTime.TryParse(model.SearchConditions["EndDate"], out result);
                    model.End = isSuccess ? result : DateTime.MaxValue;
                }


                model.QueryData();
                model.CountTotalRows();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
