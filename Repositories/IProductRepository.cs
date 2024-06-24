using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IProductRepository
    {
        public List<TblProduct> GetAllProducts();
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        //Task<List<(TblProduct product, double price)>> GetAllProductsAndPricesAsync();
        
        Task<TblProduct> GetProductByIdAsync(string productId);
        public TblProduct GetProduct(string id);
        public List<TblProduct> filterProductsByCategoryID(string categoryID);
        public List<TblProduct> GetProductsByName(string name);
        public Task<bool> UpdateProduct(string productID, TblProduct product);

        Task AddAsync(TblProduct product);
        Task SaveChangesAsync();
    }
}
