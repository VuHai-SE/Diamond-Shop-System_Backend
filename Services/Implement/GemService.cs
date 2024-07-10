using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class GemService : IGemService
    {
        public readonly IGemRepository _gemRepository;

        public GemService(IGemRepository gemRepository)
        {
            _gemRepository = gemRepository;
        }

        public TblGem AddGem(TblGem gem)
            => _gemRepository.AddGem(gem);

        public TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report)
            => _gemRepository.AddDiamondGradingReport(report);

        public bool GemExists(string gemId)
        {
            return _gemRepository.GemExists(gemId);
        }

        public bool IsGemInProduct(string gemId)
        {
            return _gemRepository.IsGemInProduct(gemId);
        }

        public void DeleteDiamondGradingReport(string gemId)
        {
            _gemRepository.DeleteDiamondGradingReport(gemId);
        }

        public void DeleteGem(string gemId)
        {
            _gemRepository.DeleteGem(gemId);
        }

        public TblGem GetGem(string gemId)
            => _gemRepository.GetGem(gemId);

        public TblGem GetGemByProduct(string productId)
            => _gemRepository.GetGemByProduct(productId);

        public List<TblGem> GetGems()
            => _gemRepository.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => _gemRepository.UpdateGem(id, gem);

        public TblDiamondGradingReport GetDiamondGradingReportByGemId(string gemId)
        {
            try
            {
                return _gemRepository.GetDiamondGradingReportByGemId(gemId);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public void UpdateDiamondGradingReport(TblDiamondGradingReport report)
        {
            _gemRepository.UpdateDiamondGradingReport(report);
        }

    }
}
