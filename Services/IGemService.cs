﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IGemService
    {
        List<TblGem> GetGems();
        TblGem GetGem(string gemId);
        TblGem AddGem(TblGem gem);
        TblDiamondGradingReport AddDiamondGradingReport(TblDiamondGradingReport report);
        bool UpdateGem(string id, TblGem gem);
        bool DeleteGem(string id);
        TblGem GetGemByProduct(string productId);
    }
}
