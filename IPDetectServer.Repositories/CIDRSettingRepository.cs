using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPDetectServer.Models;
using IPDetectServer.Lib.Exceptions;
using IPDetectServer.Lib.Common;

namespace IPDetectServer.Repositories
{
    public class CIDRSettingRepository
    {
        public CIDRSettingModel Add(CIDRSettingModel model)
        {
            if (model == null)
            {
                throw new MobileException("非法的CIDRSettingModel参数,model 不能为空.");
            }
            model.ID = Guid.NewGuid().ToString();
            cidrsetting item = new cidrsetting
            {
                ID = model.ID,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                LastUpdatedBy = model.CreatedBy,
                LastUpdatedDate = DateTime.Now,
                IPStart = model.IPStart,
                IPStartNum = IPHelper.IPToNumber(model.IPStart),
                IPEnd = model.IPEnd,
                IPEndNum = IPHelper.IPToNumber(model.IPEnd),
                TCPPort = model.TCPPort,
                TTLThreshold = model.TTLFaZhi,
                TCPThreshold = model.TCPFaZhi,
                TTLExceptionKeys = model.TTLExceptionKeys
            };

            using (var dbContext = new DataEntities())
            {
                dbContext.cidrsettings.AddObject(item);
                dbContext.SaveChanges();
            }

            return model;
        }

        public void UpdateSetting(CIDRSettingModel model)
        {
            if (model == null || String.IsNullOrWhiteSpace(model.ID))
            {
                throw new Exception("参数CIDRSettingModel不能为空。");
            }

            using (var dbContext = new DataEntities())
            {
                var dbSetting = dbContext.cidrsettings.Where(m => m.ID.Equals(model.ID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (dbSetting == null)
                {
                    throw new Exception("该CIDR设置不存在！");
                }
                else
                {
                    dbSetting.LastUpdatedBy = model.LastUpdatedBy;
                    dbSetting.LastUpdatedDate = DateTime.Now;
                    dbSetting.IPStart = model.IPStart;
                    dbSetting.IPEnd = model.IPEnd;
                    dbSetting.TCPPort = model.TCPPort;
                    dbSetting.IPStartNum = IPHelper.IPToNumber(model.IPStart);
                    dbSetting.IPEndNum = IPHelper.IPToNumber(model.IPEnd);
                    dbSetting.TTLThreshold = model.TTLFaZhi;
                    dbSetting.TCPThreshold = model.TCPFaZhi;
                    dbSetting.TTLExceptionKeys = model.TTLExceptionKeys;
                    dbContext.SaveChanges();
                }
            }
        }

        public void DeleteSetting(string id)
        {
            using (var dbContext = new DataEntities())
            {
                var dbSetting = dbContext.cidrsettings.Where(c => c.ID.Equals(id, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (dbSetting != null)
                {
                    dbContext.DeleteObject(dbSetting);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<CIDRSettingModel> GetSettings(int pageIndex, int pageSize)
        {
            List<CIDRSettingModel> result = new List<CIDRSettingModel>();
            List<cidrsetting> dbSettings = new List<cidrsetting>();
            using (var dbContext = new DataEntities())
            {
                dbSettings = dbContext.cidrsettings
                    .OrderBy(c => c.IPStartNum)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            foreach (var c in dbSettings)
            {
                var b = new CIDRSettingModel();
                b.CreatedBy = c.CreatedBy;
                b.CreatedDate = c.CreatedDate;
                b.ID = c.ID;
                b.IPEnd = c.IPEnd;
                b.IPStart = c.IPStart;
                b.LastUpdatedBy = c.LastUpdatedBy;
                b.LastUpdatedDate = c.LastUpdatedDate;
                b.TCPFaZhi = c.TCPThreshold;
                b.TCPPort = c.TCPPort;
                b.TTLFaZhi = c.TTLThreshold;
                b.TTLExceptionKeys = c.TTLExceptionKeys;

                result.Add(b);
            }
            
            return result;
        }

        public int GetRowTotal()
        {
            int count = 0;
            using (var dbContext = new DataEntities())
            {
                count = dbContext.cidrsettings.Count();
            }

            return count;
        }
    }
}
