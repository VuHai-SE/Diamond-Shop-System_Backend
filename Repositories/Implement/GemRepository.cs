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
        public readonly GemDAO _gemDAO;

        public GemRepository(GemDAO gemDAO)
        {
            _gemDAO = gemDAO;
        }

        public TblGem AddGem(TblGem gem)
            => _gemDAO.AddGem(gem);

        public TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report)
            => _gemDAO.AddDiamondGradingReport(report);

        public bool GemExists(string gemId)
        {
            return _gemDAO.GemExists(gemId);
        }

        public bool IsGemInProduct(string gemId)
        {
            return _gemDAO.IsGemInProduct(gemId);
        }

        public void DeleteDiamondGradingReport(string gemId)
        {
            _gemDAO.DeleteDiamondGradingReport(gemId);
        }

        public void DeleteGem(string gemId)
        {
            _gemDAO.DeleteGem(gemId);
        }

        public TblGem GetGem(string gemId)
            => _gemDAO.GetGem(gemId);

        public TblGem GetGemByProduct(string productId)
            => _gemDAO.GetGemByProduct(productId);

        public List<TblGem> GetGems()
            => _gemDAO.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => _gemDAO.UpdateGem(id, gem);

        public TblDiamondGradingReport GetDiamondGradingReportByGemId(string gemId)
        {
            return _gemDAO.GetDiamondGradingReportByGemId(gemId);
        }

        public void UpdateDiamondGradingReport(TblDiamondGradingReport report)
        {
            _gemDAO.UpdateDiamondGradingReport(report);
        }

    }
}
