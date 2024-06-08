using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services.DTOs.Response;

namespace Services
{
    public interface IProductService
    {
        //public List<TblProduct> GetProducts();
        //public TblProduct GetProduct(string id);
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        Task<List<(TblProduct product, double price)>> GetAllProductsAndPricesAsync();

        Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId);

        public TblProduct GetProduct(string id);
    }
}
