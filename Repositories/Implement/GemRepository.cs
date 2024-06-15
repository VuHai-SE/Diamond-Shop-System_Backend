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

        public bool DeleteGem(string id)
            => gemDAO.DeleteGem(id);

        public TblGem GetGem(string id)
            => gemDAO.GetGem(id);

        public TblGem GetGemByProduct(string productId)
            => gemDAO.GetGemByProduct(productId);

        public List<TblGem> GetGems()
            => gemDAO.GetGems();

        public bool UpdateGem(string id, TblGem gem)
            => gemDAO.UpdateGem(id, gem);
    }
}
