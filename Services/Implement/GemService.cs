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
        public readonly IGemRepository gemRepository;

        public GemService(IGemRepository _gemRepository)
        {
            gemRepository = _gemRepository;
        }

        public TblGem AddGem(TblGem gem)
            => gemRepository.AddGem(gem);

        public TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report)
            => gemRepository.AddDiamondGradingReport(report);

        public bool DeleteGem(string id)
            => gemRepository.DeleteGem(id);

        public TblGem GetGem(string gemId)
            => gemRepository.GetGem(gemId);

        public TblGem GetGemByProduct(string productId)
            => gemRepository.GetGemByProduct(productId);

        public List<TblGem> GetGems()
            => gemRepository.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => gemRepository.UpdateGem(id, gem);
    }
}
