using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPDetectServer.Models;
using IPDetectServer.Repositories;
using System.Net.NetworkInformation;

namespace IPDetectServer.Business
{
    public class IPQueryBiz
    {
        public List<GroupByAccountModel> GroupByAccount(string userName,DateTime start, DateTime end,int pageIndex = 0, int pageSize = 20)
        {
            List<GroupByAccountModel> result = new List<GroupByAccountModel>();
            if (userName != null)
                userName = userName.Trim();

            MonitorDataRepository mr = new MonitorDataRepository();
            var dbResult = mr.GroupByAccount(userName, start,end);

            if (dbResult != null && dbResult.Count > 0)
            {
                GroupByAccountModel tmp = new GroupByAccountModel();
                foreach (var item in dbResult)
                {
                    if (item.UserName != tmp.UserName)
                    {
                        tmp = new GroupByAccountModel();
                        tmp.UserName = item.UserName;
                        result.Add(tmp);
                    }

                    if (item.Status == 1)
                    {
                        tmp.StatusNormalCount = item.Count;
                    }
                    else if (item.Status == 2)
                    {
                        tmp.StatusUnknownCount = item.Count;
                    }
                    else if (item.Status == 3)
                    {
                        tmp.StatusExceptionCount = item.Count;
                    }
                }
            }

            result = result.OrderByDescending(r => r.Total).ToList();
            result = result.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            return result;
        }

        public List<GroupByRegionModel> GroupByRegion(string region, DateTime start, DateTime end, int pageIndex = 0, int pageSize = 20)
        {
            List<GroupByRegionModel> result = new List<GroupByRegionModel>();
            if (region != null)
                region = region.Trim();

            MonitorDataRepository mr = new MonitorDataRepository();
            var dbResult = mr.GroupByRegion(region, start, end);

            if (dbResult != null && dbResult.Count > 0)
            {
                GroupByRegionModel tmp = new GroupByRegionModel();
                foreach (var item in dbResult)
                {
                    if (item.Region != tmp.Region)
                    {
                        tmp = new GroupByRegionModel();
                        tmp.Region = item.Region;
                        result.Add(tmp);
                    }

                    if (item.Status == 1)
                    {
                        tmp.StatusNormalCount = item.Count;
                    }
                    else if (item.Status == 2)
                    {
                        tmp.StatusUnknownCount = item.Count;
                    }
                    else if (item.Status == 3)
                    {
                        tmp.StatusExceptionCount = item.Count;
                    }
                }
            }

            result = result.OrderByDescending(r => r.Total).ToList();
            result = result.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            return result;

        }

        public List<ClientIPModel> Query(string selectedRegion, string userName, DateTime start, DateTime end, int pageIndex = 1, int pageSize = 20, int status = -1)
        {
            List<ClientIPModel> result = new List<ClientIPModel>();
            MonitorDataRepository mr = new MonitorDataRepository();

            var dbResult = mr.GetMonitorDatas(selectedRegion, userName, start, end, pageIndex, pageSize, status);
            foreach (var t in dbResult)
            {
                ClientIPModel clientIP = new ClientIPModel
                {
                    ID = t.ID,
                    ClientIP = t.ClientPublicIP,
                    ClientPrivateIP = t.ClientPrivateIP,
                    ClientProvince = t.ClientProvince,
                    ClientCity = t.ClientCity,
                    ClientDistinct = t.ClientDistinct,
                    ClientAddress = t.ClientDetailAddr,
                    ClientRecordor = t.ClientRecordor,
                    ExpectedISP = t.ExpectedOperator,
                    ExpectedISPProvince = t.ExpectedOperatorProvince,
                    ExpectedISPCity = t.ExpectedOperatorCity,
                    RealISP = t.RealOperator,
                    RealISPProvince = t.RealOperatorProvince,
                    RealISPCity = t.RealOperatorCity,
                    CreatedDate = t.CreatedDate,
                    UserName = t.UserName,
                    Status = (IPMonitorStatus)t.Status
                };
                
                result.Add(clientIP);
            }
            return result;
        }

        public int CountTotalRows(string region, string userName, DateTime start, DateTime end, int status)
        {
            
            MonitorDataRepository mr = new MonitorDataRepository();
            int result = mr.GetRowCount(region, userName, start, end, status);
         
            return result;
        }
    }
}
