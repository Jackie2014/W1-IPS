using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPDetectServer.Models;

namespace IPDetectServer.Repositories
{
    public class IPScanResultRepository
    {
        public List<IPScanResult> QueryIPScanResults(DateTime start, DateTime end, string tcpStatus, string ttlStatus, int pageIndex = 0, int pageSize = 20)
        {
            List<IPScanResult> result = new List<IPScanResult>();
            List<ipscanresults> dbResult = null;
            using (var dbContext = new DataEntities())
            {
                dbResult = dbContext.ipscanresults.Where(t =>
                       (String.IsNullOrEmpty(tcpStatus) ? true : t.TCPValidation.Equals(tcpStatus, StringComparison.OrdinalIgnoreCase))
                    && (String.IsNullOrEmpty(ttlStatus) ? true : t.TTLValidation.Equals(ttlStatus, StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end))
                        .OrderBy(t => t.IP).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            foreach (var r in dbResult)
            {
                IPScanResult item = new IPScanResult
                {
                    IP = r.IP,
                    CreatedDate = r.CreatedDate,
                    TCPResponseTime = r.TCPTime,
                    TCPValidationResult = r.TCPValidation,
                    TTLValidationResult = r.TTLValidation
                };

                result.Add(item);
            }

            return result;
        }

        public int GetRowTotal(DateTime start, DateTime end, string tcpStatus, string ttlStatus)
        {
            int count = 0;
            using (var dbContext = new DataEntities())
            {
                count = dbContext.ipscanresults.Where(t =>
                       (String.IsNullOrEmpty(tcpStatus) ? true : t.TCPValidation.Equals(tcpStatus, StringComparison.OrdinalIgnoreCase))
                    && (String.IsNullOrEmpty(ttlStatus) ? true : t.TTLValidation.Equals(ttlStatus, StringComparison.OrdinalIgnoreCase))
                    && (t.CreatedDate >= start && t.CreatedDate <= end))
                    .Count();
            }

            return count;
        }
    }
}
