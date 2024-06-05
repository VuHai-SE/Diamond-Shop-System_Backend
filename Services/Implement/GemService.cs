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
        public readonly IGemRepository gemRepository = null;

        public GemService()
        {
            if (gemRepository == null)
            {
                gemRepository = new GemRepository();
            }
        }

        public TblGem AddGem(TblGem gem)
            => gemRepository.AddGem(gem);

        public bool DeleteGem(string id)
            => gemRepository.DeleteGem(id);

        public TblGem GetGem(string id)
            => gemRepository.GetGem(id);

        public List<TblGem> GetGems()
            => gemRepository.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => gemRepository.UpdateGem(id, gem);
    }
}
