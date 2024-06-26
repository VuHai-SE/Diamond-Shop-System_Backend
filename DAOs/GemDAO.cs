using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class GemDAO
    {
        private readonly DiamondStoreContext dbContext;

        public GemDAO(DiamondStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<TblGem> GetGems() => dbContext.TblGems.ToList();

        public TblGem GetGem(string id)
            => dbContext.TblGems.FirstOrDefault(m => m.GemId.Equals(id));

        public TblGem AddGem(TblGem gem)
        {
            dbContext.TblGems.Add(gem);
            dbContext.SaveChanges();
            return gem;
        }

        public bool UpdateGem(string id, TblGem gem)
        {
            return false;
        }

        public bool DeleteGem(string id)
        {
            return false;
        }

        public TblGem GetGemByProduct(string productId)
        {
            var productGem = dbContext.TblProductGems.FirstOrDefault(pg => pg.ProductId.Equals(productId));
            return dbContext.TblGems.FirstOrDefault(g => g.GemId.Equals(productGem.GemId));
        }
    }
}
