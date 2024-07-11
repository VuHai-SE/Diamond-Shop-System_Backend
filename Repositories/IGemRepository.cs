using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IGemRepository
    {
        List<TblGem> GetGems();
        TblGem GetGem(string gemId);
        TblGem AddGem(TblGem gem);
        TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report);
        bool UpdateGem(string id, TblGem gem);
        public bool GemExists(string gemId);
        public bool IsGemInProduct(string gemId);
        public void DeleteDiamondGradingReport(string gemId);
        public void DeleteGem(string gemId);
        TblGem GetGemByProduct(string productId);
        public TblDiamondGradingReport GetDiamondGradingReportByGemId(string gemId);
        public void UpdateDiamondGradingReport(TblDiamondGradingReport report);

    }
}
