using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPDetectServer.Web.Models;
using IPDetectServer.Web.ViewModels;
using IPDetectServer.Models;
using IPDetectServer.Repositories;

namespace IPDetectServer.Web.Controllers
{
    public class CIDRSettingController : CustomizedControllerBase
    {

        public ActionResult Index()
        {
            CIDRManageViewModel model = new CIDRManageViewModel();
            this.RefreshData(true, model);
            return View(model);
        }

        public ActionResult New()
        {
            CIDRSettingModel model = new CIDRSettingModel();
            model.TCPPort = 80;
            model.TCPFaZhi = 50;
            model.TTLFaZhi = 10;
            return View("New", model);
        }

        [HttpPost]
        [AcceptButton("btnSortInGrid")]
        public ActionResult SortAction(CIDRManageViewModel model)
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
        public ActionResult PagingAction(CIDRManageViewModel model)
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


        [HttpPost]
        [AcceptButton("btnAdd")]
        public ActionResult Add(CIDRSettingModel model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.IPStart)
                    || String.IsNullOrWhiteSpace(model.IPEnd)
                    )
                {
                    throw new Exception("打 * 的为必填字段，请正确填写完毕再提交。");
                }

                if (model.TCPPort < 0 || model.TCPPort > 65535)
                {
                    throw new Exception("TCP端口必须在0-65535之间。");
                }

                if (model.TCPFaZhi < 1 || model.TCPFaZhi > 10000)
                {
                    throw new Exception("TCP响应时间阀值必须在1-10000之间。");
                }

                if (model.TTLFaZhi < 1 || model.TTLFaZhi > 255)
                {
                    throw new Exception("TCP响应时间阀值必须在1-255之间。");
                }

                if (!IsValidIP(model.IPStart))
                {
                    throw new Exception("开始地址不是一个有效的IP地址。");
                }

                if (!IsValidIP(model.IPEnd))
                {
                    throw new Exception("结束地址不是一个有效的IP地址。");
                }

                if (!ValidateIP( model.IPStart, model.IPEnd))
                {
                    throw new Exception("IP结束地址必须大于开始地址。");
                }

                model.CreatedBy = SessionManager.User.LoginName;
                CIDRSettingRepository rep = new CIDRSettingRepository();
                rep.Add(model);

                ModelState.AddModelError("", "添加IP段成功。");
                return View("New", new CIDRSettingModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("New", new CIDRSettingModel());
            }
        }

        public bool IsValidIP(string ip)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = ip.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        return true;
                    else
                        return false;
                }
                else
                    return false;

            }
            else
                return false;
        }

        /// <summary>
        /// 确保结束ip大于开始ip
        /// </summary>
        private bool ValidateIP(string startIP, string endIP)
        {
            // 分离出ip中的四个数字位
            string[] startIPArray = startIP.Split('.');
            string[] endIPArray = endIP.Split('.');

            // 取得各个数字
            long[] startIPNum = new long[4];
            long[] endIPNum = new long[4];
            for (int i = 0; i < 4; i++)
            {
                startIPNum[i] = long.Parse(startIPArray[i].Trim());
                endIPNum[i] = long.Parse(endIPArray[i].Trim());
            }

            // 各个数字乘以相应的数量级
            long startIPNumTotal = startIPNum[0] * 256 * 256 * 256 + startIPNum[1] * 256 * 256 + startIPNum[2] * 256 + startIPNum[3];
            long endIPNumTotal = endIPNum[0] * 256 * 256 * 256 + endIPNum[1] * 256 * 256 + endIPNum[2] * 256 + endIPNum[3];

            if (startIPNumTotal > endIPNumTotal)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected ViewResult RefreshData(bool needReFetchRecordCount, CIDRManageViewModel model)
        {
            if (needReFetchRecordCount)
            {
                model.CountTotalRows();
                ModelState.Remove("TotalRows");
            }
            
            model.QueryData();
            return View(model);
        }
    }
}
