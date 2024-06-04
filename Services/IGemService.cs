using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IGemService
    {
        public List<TblGem> GetGems();
        public TblGem GetGem(string id);
        public TblGem AddGem(TblGem gem);
        public bool UpdateGem(string id, TblGem gem);
        public bool DeleteGem(string id);
    }
}
