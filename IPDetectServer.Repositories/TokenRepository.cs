using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDetectServer.Repositories
{
    public class TokenRepository
    {
        public string NewToken(token tokenModel)
        {
            tokenModel.Token = Guid.NewGuid().ToString("N");
            tokenModel.CreatedDate = DateTime.Now;
            tokenModel.ExpiredDate = DateTime.Now.AddYears(1);

            using (var dbContext = new DataEntities())
            {
                dbContext.Tokens.AddObject(tokenModel);
                dbContext.SaveChanges();
            }

            return tokenModel.Token;
        }

        public token GetTokenModel(string token)
        {
            token tokenModel = null;

            using (var dbContext = new DataEntities())
            {
                tokenModel = dbContext.Tokens.Where(t => t.Token == token).FirstOrDefault();
            }

            return tokenModel;
        }
    }
}
