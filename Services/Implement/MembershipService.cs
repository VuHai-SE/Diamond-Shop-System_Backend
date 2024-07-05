using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services.Implement
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipService(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }
        public TblMembership GetMembershipByID(int id)
            => _membershipRepository.GetMembershipByID(id);

        public TblMembership GetMemberShipByRank(string rank)
            => _membershipRepository.GetMemberShipByRank(rank);

        public List<TblMembership> GetMembershipList()
            => _membershipRepository.GetMembershipList();
    }
}
