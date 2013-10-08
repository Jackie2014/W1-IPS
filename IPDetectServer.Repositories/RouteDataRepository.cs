using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Repositories
{
    public class RouteDataRepository
    {
        public List<RouteData> AddRoutDatas(List<RouteData> routeDatas)
        {
            if (routeDatas == null || routeDatas.Count == 0)
            {
                return routeDatas;
            }

            using (var dbContext = new DataEntities())
            {
                foreach (var item in routeDatas)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    item.CreatedDate = DateTime.Now;
                    item.LastUpdatedDate = DateTime.Now;
                    dbContext.RouteDatas.AddObject(item);
                }

                dbContext.SaveChanges();
            }

            return routeDatas;
        }
    }
}
