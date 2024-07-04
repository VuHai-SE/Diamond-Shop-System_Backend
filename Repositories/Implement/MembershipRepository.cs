using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;

namespace Repositories.Implement
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly MembershipDAO _membershipDAO;

        public MembershipRepository(MembershipDAO membershipDAO)
        {
            _membershipDAO = membershipDAO;
        }

        public TblMembership GetMembershipByID(int id)
            => _membershipDAO.GetMembershipByID(id);

        public TblMembership GetMemberShipByRank(string rank)
            => _membershipDAO.GetMemberShipByRank(rank);

        public List<TblMembership> GetMembershipList()
            => _membershipDAO.GetMembershipList();
    }
}
