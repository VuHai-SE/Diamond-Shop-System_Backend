using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DAOs
{
    public class MembershipDAO
    {
        private readonly DiamondStoreContext _dbContext;

        public MembershipDAO(DiamondStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TblMembership GetMembershipByID(int id)
            => _dbContext.TblMemberships.FirstOrDefault(m => m.Id.Equals(id));

        public TblMembership GetMemberShipByRank(string rank)
            => _dbContext.TblMemberships.FirstOrDefault(m => m.Ranking.Equals(rank));

        public List<TblMembership> GetMembershipList()
            => _dbContext.TblMemberships.ToList();
    }
}
