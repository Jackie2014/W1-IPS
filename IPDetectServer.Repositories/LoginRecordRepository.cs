using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPDetectServer.Repositories
{
    public class LoginRecordRepository
    {
        public void Add(LoginRecord loginRecord)
        {
            if (loginRecord == null)
            {
                return;
            }

            loginRecord.LoginDate = DateTime.Now;

            using (var dbContext = new DataEntities())
            {
                dbContext.LoginRecords.AddObject(loginRecord);
                dbContext.SaveChanges();
            }
        }

        public List<LoginRecord> Query(string userName, DateTime loginDateStart, DateTime loginDateEnd)
        {
            List<LoginRecord> result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.LoginRecords.Where(t => 
                       t.UserName.Contains(userName) 
                    && t.LoginDate >= loginDateStart 
                    && t.LoginDate <= loginDateEnd).ToList();
            }

            return result;
        }

        public LoginRecord GetLatestLoginRecord(string userName)
        {
            LoginRecord result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.LoginRecords
                        .Where(t =>t.UserName.Equals(userName,StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(t=>t.LoginDate)
                        .FirstOrDefault();
                    
            }

            return result;
        }
    }
}
