using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IPDetectServer.Repositories
{
    public class IPRepository
    {
        public IPEntity RetriveIP(string ip)
        {
            if(!IsIPAddress(ip))
            {
                return null;
            }

            long ipNum = ConvertIPToNumber(ip); ;
            IPEntity result = null;

            using (var dbContext = new DataEntities())
            {
                result = dbContext.IPEntities.Where(t => ipNum >= t.IPStartNum && ipNum <= t.IPEndNum).FirstOrDefault();
            }

            return result;
        }

        private long ConvertIPToNumber(string ip)
        {
            string[] ips = ip.Split(new char[] { '.' });
            if (ips.Length != 4) return 0;
            return Convert.ToInt64(Convert.ToInt64(ips[0]) * Math.Pow(256, 3)
                + (Convert.ToInt64(ips[1]) * Math.Pow(256, 2))
                + (Convert.ToInt64(ips[2]) * 256)
                + Convert.ToInt64(ips[3]));
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15) return false;
            const string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
    }
}
