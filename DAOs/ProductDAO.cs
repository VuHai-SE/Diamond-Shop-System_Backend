using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ProductDAO
    {
        private readonly DiamondStoreContext _context = null;

        public ProductDAO()
        {
            _context = new DiamondStoreContext();
        }

        public TblProduct AddProduct(TblProduct product)
        {
            _context.TblProducts.Add(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<TblProduct> GetProductByIdAsync(string productId)
        {
            return await _context.TblProducts.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<TblGem>> GetGemsByProductIdAsync(string productId)
        {
            return await (from gem in _context.TblGems
                          join productGem in _context.TblProductGems on gem.GemId equals productGem.GemId
                          where productGem.ProductId == productId
                          select gem).AsNoTracking().ToListAsync();
        }

        public IQueryable<TblGemPriceList> GetGemPriceList(TblGem gem)
        {
            return _context.TblGemPriceLists
                .Where(gpl => gpl.Origin == gem.Origin &&
                              gpl.CaratWeight == gem.CaratWeight &&
                              gpl.Color == gem.Color &&
                              gpl.Cut == gem.Cut &&
                              gpl.Clarity == gem.Clarity)
                .OrderByDescending(gpl => gpl.EffDate).AsNoTracking();
        }

        public async Task<List<TblProductMaterial>> GetProductMaterialsAsync(string productId)
        {
            return await _context.TblProductMaterials
                .Where(pm => pm.ProductId == productId)
                .AsNoTracking().ToListAsync();
        }

        public IQueryable<TblMaterialPriceList> GetMaterialPriceList(string materialId)
        {
            return _context.TblMaterialPriceLists
                .Where(mpl => mpl.MaterialId == materialId)
                .OrderByDescending(mpl => mpl.EffDate)
                .AsNoTracking();
        }

        //Get All Products
        public List<TblProduct> GetAllProducts()
        {
            return _context.TblProducts.ToList();
        }

        //Get product detail
        public TblProduct GetProduct(string id)
        {
            return _context.TblProducts.FirstOrDefault(m => m.ProductId.Equals(id));
        }
    }
}
