using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IMembershipService
    {
        public TblMembership GetMembershipByID(int id);
        public TblMembership GetMemberShipByRank(string rank);
        public List<TblMembership> GetMembershipList();
    }
}
