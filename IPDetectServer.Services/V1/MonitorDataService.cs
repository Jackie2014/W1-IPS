using AttributeRouting;
using AttributeRouting.Web.Http;
using IPDetectServer.Lib.Exceptions;
using IPDetectServer.Lib;
using IPDetectServer.Lib.Encrypt;
using IPDetectServer.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using IPDetectServer.Lib.Common;
using IPDetectServer.Models;
using IPDetectServer.Models.WS;
using IPDetectServer.Business;

namespace IPDetectServer.Services
{
    [RoutePrefix("apis/v1/monitordata")]
    public class MonitorDataController : ApiController
    {
        private string CenterServerBaseUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["CenterServerBaseUrl"];
                if (!url.EndsWith("/")) url += "/";

                return url;
            }
        }

        /// <summary>
        /// 监测服务器接收到客户端发生的IP检测请求，
        /// 返回客户端的请求的IP地址同时转发收集的监控数据到中心服务器。
        /// </summary>
        /// <param name="monitorData">客户端请求的数据。</param>
        [POST("")]
        public MonitorDataResponse ClientSubmitToMonitorServer([FromBody]MonitorDataRequest monitorData)
        {
            if (monitorData == null)
            {
                throw new BadRequestException("请求的数据不能为空。");
            }

            MonitorDataResponse response = new MonitorDataResponse();

            response.IP = Context.ClientIP;
            monitorData.ClientPublicIP = Context.ClientIP;
            
            string token = Context.Token;
            //Task.Factory.StartNew(() =>
            //{
                try
                {
                    RestClient restClient = new RestClient();
                    restClient.BaseUrl = CenterServerBaseUrl;
                    IRestRequest request = new RestRequest("apis/v1/monitordata/transfer", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", token);
                    request.AddBody(monitorData);
                    var responseFromServer = restClient.Execute<MonitorDataResponse>(request);
                    if (responseFromServer != null && responseFromServer.Data != null)
                    {
                        response.UID = responseFromServer.Data.UID;
                        response.RealOperator = responseFromServer.Data.RealOperator;
                        response.RealOperatorProvince = responseFromServer.Data.RealOperatorProvince;
                        response.RealOperatorCity = responseFromServer.Data.RealOperatorCity;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            //});

            return response;
        }

        /// <summary>
        /// 监测服务器转交IP监控数据到中心主服务器。
        /// </summary>
        /// <param name="monitorData">监测服务器转交的数据。</param>
        [POST("transfer")]
        public MonitorDataResponse MoniterServerSubmitToCenterrServer([FromBody]MonitorDataRequest monitorData)
        {
            if (monitorData == null)
            {
                throw new BadRequestException();
            }

            // 查询客户端IP所属的运营商
            IPRepository ipRep = new IPRepository();
            var ipEntity = ipRep.RetriveIP(monitorData.ClientPublicIP);

            string realOperator = ipEntity == null ? null : ipEntity.IPBelongTo;

            string realOperatorProvince = null;
            string realOperatorCity = null;
            if (ipEntity != null)
            {
                realOperatorProvince = ipEntity.Province;
                realOperatorCity = ipEntity.City;
            }

            bool isSameProvice = false;

            if(String.IsNullOrWhiteSpace(realOperatorProvince) && String.IsNullOrWhiteSpace(monitorData.ExpectedOperatorProvice))
            {
                isSameProvice = true;
            }
            else if(monitorData.ExpectedOperatorProvice.Equals(realOperatorProvince))
            {
                isSameProvice = true;
            }

            var status = IPMonitorStatus.Pending;

            if (!String.IsNullOrEmpty(realOperator))
            {
                realOperator = realOperator.Trim();
            }

            if (String.IsNullOrEmpty(realOperator))
            {
                if (ipEntity != null)
                {
                    realOperator = ipEntity.IPBelongTo = ipEntity.Country;
                }
                status = IPMonitorStatus.Unknown;
            }
            else if (isSameProvice && isISPMatched(monitorData.ExpectedOperator,realOperator))
            {
                status = IPMonitorStatus.Normal;
            }
            else
            {
                status = IPMonitorStatus.Exception;
            }

            ClientIP clientIP = new ClientIP
            {
                ClientProvince = monitorData.ClientProvince,
                ClientCity = monitorData.ClientCity,
                ClientDistinct = monitorData.ClientDistinct,
                ClientDetailAddr = monitorData.ClientDetailAddress,
                ClientRecordor = monitorData.ClientRecordor,
                ClientPublicIP = monitorData.ClientPublicIP,
                ClientPrivateIP = monitorData.ClientPrivateIP,
                SubmitFromServerIP = Context.ClientIP,
                Status = (int)status,
                ExpectedOperatorProvince = monitorData.ExpectedOperatorProvice,
                ExpectedOperatorCity = monitorData.ExpectedOperatorCity,
                ExpectedOperator = monitorData.ExpectedOperator,
                RealOperator = realOperator,
                RealOperatorProvince = realOperatorProvince,
                RealOperatorCity = realOperatorCity,
                UserName = Context.LoginName
            };

            MonitorDataRepository mdr = new MonitorDataRepository();
            clientIP = mdr.Add(clientIP);

            MonitorDataResponse response = new MonitorDataResponse();
            response.UID = clientIP.ID;
            response.RealOperator = realOperator;
            response.RealOperatorProvince = realOperatorProvince;
            response.RealOperatorCity = realOperatorCity;

            return response;
        }

        /// <summary>
        /// 客户端提交路由跟踪数据到服务器。
        /// </summary>
        /// <param name="RouteDataRequestItem">路由数据列表。</param>
        [POST("route")]
        public List<RouteDataResponseItem> ClientSubmitRouteDataToCenterrServer([FromBody]List<RouteDataRequestItem> routeDatas)
        {
            if (routeDatas == null)
            {
                throw new BadRequestException();
            }

            List<RouteData> dbItems = new List<RouteData>();
            
            IPRepository ipr = new IPRepository();

            foreach (var item in routeDatas)
            {
                if (item == null)
                {
                    continue;
                }

                RouteData dbItem = new RouteData();
                dbItem.GroupID = item.ParentUID;
                dbItem.T1 = item.T1;
                dbItem.T2 = item.T2;
                dbItem.T3 = item.T3;
                dbItem.RouteIP = item.RouteIP;
                dbItem.RouteDate = item.RouteDate;
                dbItem.SeqNoInGroup = item.SeqNo;
                dbItem.CreatedBy = Context.LoginName;
                dbItem.LastUpdatedBy = Context.LoginName;

                string ipBelongTo = "";
                if (!String.IsNullOrWhiteSpace(item.RouteIP))
                {
                    var ipEntity = ipr.RetriveIP(item.RouteIP);

                    if (ipEntity != null)
                    {
                        ipBelongTo = String.IsNullOrWhiteSpace(ipEntity.IPBelongTo) ? ipEntity.Country : ipEntity.IPBelongTo;
                        dbItem.IPBelongTo = ipBelongTo;
                        dbItem.IPBelongToProvince = ipEntity.Province;
                        dbItem.IPBelongToCity = ipEntity.City;
                    }
                }

                dbItems.Add(dbItem);
            }

            RouteDataRepository rdr = new RouteDataRepository();
            var resultDbItems = rdr.AddRoutDatas(dbItems);

            List<RouteDataResponseItem> result = new List<RouteDataResponseItem>();
            foreach (var item in resultDbItems)
            {
                RouteDataResponseItem rItem = new RouteDataResponseItem();
                rItem.ParentUID = item.GroupID;
                rItem.SeqNo = (item.SeqNoInGroup.HasValue ? item.SeqNoInGroup.Value : 0);
                rItem.RouteIP = item.RouteIP;
                rItem.IPBelongTo = item.IPBelongTo;
                rItem.IPBelongToProvince = item.IPBelongToProvince;
                rItem.IPBelongToCity = item.IPBelongToCity;
                result.Add(rItem);
            }

            return result;
        }

        /// <summary>
        /// 日志查询
        /// </summary>
        /// <returns></returns>
        [POST("logs")]
        public List<ClientIPModel> LogQuery()
        {
            string userName = Context.LoginName;

            if (String.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            IPQueryBiz biz = new IPQueryBiz();
            var result = biz.Query(null, userName, DateTime.MinValue, DateTime.MaxValue, 0, Int32.MaxValue);
            return result;
        }

        private bool isISPMatched(string isp1, string isp2)
        {
            if (String.IsNullOrWhiteSpace(isp1) || String.IsNullOrWhiteSpace(isp2))
            {
                return false;
            }

            string groupsString = ConfigurationManager.AppSettings["ISPGroup"];

            if (String.IsNullOrWhiteSpace(groupsString))
            {
                if(isp1.IndexOf(isp2,StringComparison.OrdinalIgnoreCase) > -1
                    || isp2.IndexOf(isp1, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool result = false;
            var groups = groupsString.Split(new char[] { ',' });

            foreach (string g in groups)
            {
                if (String.IsNullOrWhiteSpace(g))
                {
                    continue;
                }

                var groupItems = g.Split(new char[] { '|' });

                bool isp1Matched = false;
                bool isp2Matched = false;
                foreach (string isp in groupItems)
                {
                    if (!String.IsNullOrWhiteSpace(isp))
                    {
                        if (isp1.IndexOf(isp, StringComparison.OrdinalIgnoreCase) > -1) 
                        {
                            isp1Matched = true;
                        }

                        if (isp2.IndexOf(isp, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            isp2Matched = true;
                        }
                    }
                }

                if (isp1Matched && isp2Matched)
                {
                    result = true;
                    break;
                }
            }

            if (result == false)
            {
                if (isp1.IndexOf(isp2, StringComparison.OrdinalIgnoreCase) > -1
                    || isp2.IndexOf(isp1, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
