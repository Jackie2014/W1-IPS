using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPDetectServer.Lib.Exceptions;

namespace IPDetectServer.Repositories
{
    public class UserRepository
    {
        public User AddUser(User user)
        {
            if (user == null)
            {
                throw new MobileException("非法的user参数,user 不能为空.");
            }

            using (var dbContext = new DataEntities())
            {
                user.CreatedDate = DateTime.Now;
                user.LastUpdatedDate = DateTime.Now;
                dbContext.Users.AddObject(user);
                dbContext.SaveChanges();
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            if (user == null || String.IsNullOrWhiteSpace(user.UserName))
            {
                throw new Exception("参数userName不能为空。");
            }

            using (var dbContext = new DataEntities())
            {
                var dbUser = dbContext.Users.Where(u => u.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (dbUser == null)
                {
                    throw new Exception("该用户不存在！");
                }
                else
                {
                    dbUser.FullName = user.FullName;
                    dbUser.Phone = user.Phone;
                    dbUser.Description = user.Description;
                    dbUser.City = user.City;
                    dbContext.SaveChanges();
                }
            }
        }

        public void DeleteUser(string userId)
        {
            using (var dbContext = new DataEntities())
            {
                var dbUser = dbContext.Users.Where(u => u.UserName.Equals(userId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (dbUser != null)
                {
                    dbContext.DeleteObject(dbUser);
                    dbContext.SaveChanges();
                }
            }
        }

        public void ChangePassword(string userName, string newPassword)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new Exception("参数userName不能为空。");
            }

            using (var dbContext = new DataEntities())
            {
                var dbUser = dbContext.Users.Where(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (dbUser == null)
                {
                    throw new Exception("该用户不存在！");
                }
                else
                {
                    dbUser.Password = newPassword;
                    dbContext.SaveChanges();
                }
            }

        }

        public User GetUser(string loginName)
        {
            User user = null;

            using (var dbContext = new DataEntities())
            {
                user = dbContext.Users.Where(u => u.UserName.Equals(loginName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            return user;
        }

        public IList<User> GetUsers(int pageIndex,int pageSize)
        {
            IList<User> result = null;
            using (var dbContext = new DataEntities())
            {
                result = dbContext.Users
                    .OrderBy(u=>u.UserName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return result;
        }

        public User Login(string userName, string md5Password)
        {
            User user = null;

            using (var dbContext = new DataEntities())
            {
                user = (from u in dbContext.Users 
                             where u.UserName.Equals(userName) && u.Password.Equals(md5Password)
                                select u).FirstOrDefault();
            }

            return user;
        }

    }
}
