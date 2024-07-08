using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class GemRepository : IGemRepository
    {
        public readonly GemDAO gemDAO;

        public GemRepository(GemDAO _gemDAO)
        {
            gemDAO = _gemDAO;
        }

        public TblGem AddGem(TblGem gem)
            => gemDAO.AddGem(gem);

        public TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report)
            => gemDAO.AddDiamondGradingReport(report);

        public bool GemExists(string gemId)
        {
            return gemDAO.GemExists(gemId);
        }

        public bool IsGemInProduct(string gemId)
        {
            return gemDAO.IsGemInProduct(gemId);
        }

        public void DeleteDiamondGradingReport(string gemId)
        {
            gemDAO.DeleteDiamondGradingReport(gemId);
        }

        public void DeleteGem(string gemId)
        {
            gemDAO.DeleteGem(gemId);
        }

        public TblGem GetGem(string gemId)
            => gemDAO.GetGem(gemId);

        public TblGem GetGemByProduct(string productId)
            => gemDAO.GetGemByProduct(productId);

        public List<TblGem> GetGems()
            => gemDAO.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => gemDAO.UpdateGem(id, gem);

        public TblDiamondGradingReport GetDiamondGradingReportByGemId(string gemId)
        {
            return gemDAO.GetDiamondGradingReportByGemId(gemId);
        }

        public void UpdateDiamondGradingReport(TblDiamondGradingReport report)
        {
            gemDAO.UpdateDiamondGradingReport(report);
        }

    }
}
