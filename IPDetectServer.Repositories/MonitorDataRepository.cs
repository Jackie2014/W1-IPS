using IPDetectServer.Lib;
using IPDetectServer.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDetectServer.Repositories
{
    public class MonitorDataRepository
    {
        public ClientIP Add(ClientIP clientIP)
        {
            clientIP.CreatedDate = DateTime.Now;
            clientIP.LastUpdatedDate = DateTime.Now;

            using (var dbContext = new DataEntities())
            {
                dbContext.ClientIPs.AddObject(clientIP);
                dbContext.SaveChanges();
            }

            return clientIP;
        }

        public IList<ClientIP> GetMonitorDatas(string selectedRegion, string userName, DateTime start,DateTime end,int pageIndex = 0, int pageSize = 20, int status = -1)
        {
            IList<ClientIP> result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.ClientIPs.Where(t=> 
                       (String.IsNullOrEmpty(userName)? true : t.UserName.Equals(userName,StringComparison.OrdinalIgnoreCase))
                    && (String.IsNullOrEmpty(selectedRegion) ? true : t.ClientCity.Equals(selectedRegion,StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end)
                    && (status == -1 ? true : t.Status == status ))
                    .OrderByDescending(t=>t.CreatedDate)
                    .Skip(pageIndex*pageSize).Take(pageSize).ToList();
            }

            return result;
        }

        public int GetRowCount(string selectedRegion, string userName, DateTime start, DateTime end, int status)
        {
            int count = 0;
            using (var dbContext = new DataEntities())
            {
                count = dbContext.ClientIPs.Where(t =>
                       (String.IsNullOrEmpty(userName) ? true : t.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                    && (String.IsNullOrEmpty(selectedRegion) ? true : t.ClientCity.Equals(selectedRegion, StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end)
                    && (status == -1 ? true : t.Status == status))
                    .Count();
            }

            return count;
        }

        public List<GroupByAccountAndStatusModel> GroupByAccount(string userName, DateTime start, DateTime end)
        {
            List<GroupByAccountAndStatusModel> result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.ClientIPs.
                    Where(t => (String.IsNullOrEmpty(userName) ? true : t.UserName.Equals(userName,StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end))
                    .GroupBy(g=>new {g.UserName,g.Status})
                    .OrderBy(g=>g.Key.UserName)
                    .Select(g => new GroupByAccountAndStatusModel
                    { 
                        UserName = g.Key.UserName,
                        Status = g.Key.Status.HasValue ? g.Key.Status.Value : -1,
                        Count = g.Count()
                    })
                    .ToList();
            }

            return result;
        }

        public List<GroupByRegionAndStatusModel> GroupByRegion(string province, DateTime start, DateTime end)
        {
            List<GroupByRegionAndStatusModel> result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.ClientIPs.
                    Where(t => (String.IsNullOrEmpty(province) ? true : t.ClientProvince.Equals(province, StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end))
                    .GroupBy(g => new { g.ClientCity, g.Status })
                    .OrderBy(g => g.Key.ClientCity)
                    .Select(g => new GroupByRegionAndStatusModel
                    {
                        Region = g.Key.ClientCity,
                        Status = g.Key.Status.HasValue ? g.Key.Status.Value : -1,
                        Count = g.Count()
                    })
                    .ToList();
            }

            return result;
        }
    }
}
