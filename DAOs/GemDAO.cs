using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Extensions.Logging;

namespace DAOs
{
    public class GemDAO
    {
        private readonly DiamondStoreContext dbContext;
        private readonly ILogger<GemDAO> _logger;

        public GemDAO(DiamondStoreContext _dbContext, ILogger<GemDAO> logger)
        {
            dbContext = _dbContext;
            _logger = logger;
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
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException(nameof(productId), "Product ID cannot be null or empty");
            }

            var productGem = dbContext.TblProductGems.FirstOrDefault(pg => pg.ProductId.Equals(productId));
            if (productGem == null)
            {
                _logger.LogError("No product gem found for product ID: {ProductId}", productId);
                return null;
            }

            var gem = dbContext.TblGems.FirstOrDefault(g => g.GemId.Equals(productGem.GemId));
            if (gem == null)
            {
                _logger.LogError("No gem found for gem ID: {GemId}", productGem.GemId);
            }

            return gem;
        }
    }
}
