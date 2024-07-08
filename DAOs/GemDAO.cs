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

        public TblGem GetGem(string gemId)
            => dbContext.TblGems.FirstOrDefault(g => g.GemId == gemId);

        public TblGem AddGem(TblGem gem)
        {
            dbContext.TblGems.Add(gem);
            dbContext.SaveChanges();
            return gem;
        }

        public TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report)
        {
            dbContext.TblDiamondGradingReports.Add(report);
            dbContext.SaveChanges();
            return report;
        }

        public bool GemExists(string gemId)
        {
            return dbContext.TblGems.Any(g => g.GemId == gemId);
        }

        public bool IsGemInProduct(string gemId)
        {
            return dbContext.TblProductGems.Any(pg => pg.GemId == gemId);
        }

        public void DeleteDiamondGradingReport(string gemId)
        {
            var report = dbContext.TblDiamondGradingReports.FirstOrDefault(r => r.GemId == gemId);
            if (report != null)
            {
                dbContext.TblDiamondGradingReports.Remove(report);
                dbContext.SaveChanges();
            }
        }

        public void DeleteGem(string gemId)
        {
            var gem = dbContext.TblGems.FirstOrDefault(g => g.GemId == gemId);
            if (gem != null)
            {
                dbContext.TblGems.Remove(gem);
                dbContext.SaveChanges();
            }
        }

        public bool UpdateGem(string id, TblGem gem)
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

        public TblDiamondGradingReport GetDiamondGradingReportByGemId(string gemId)
        {
            var report = dbContext.TblDiamondGradingReports.FirstOrDefault(r => r.GemId == gemId);
            if (report == null)
            {
                throw new KeyNotFoundException($"No Diamond Grading Report found for GemId: {gemId}");
            }
            return report;
        }


        public void UpdateDiamondGradingReport(TblDiamondGradingReport report)
        {
            dbContext.TblDiamondGradingReports.Update(report);
            dbContext.SaveChanges();
        }

    }
}
