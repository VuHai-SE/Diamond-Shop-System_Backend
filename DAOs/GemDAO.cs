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
        private readonly DiamondStoreContext dbContext = null;

        public GemDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
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
    }
}
