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
        //public List<TblProduct> GetProducts();
        //public TblProduct GetProduct(string id);
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        Task<List<(TblProduct product, double price)>> GetAllProductsAndPricesAsync();

        Task<TblProduct> GetProductByIdAsync(string productId);
        public TblProduct GetProduct(string id);
    }
}
