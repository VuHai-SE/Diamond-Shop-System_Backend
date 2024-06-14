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
        public TblProduct AddProduct(TblProduct product);

        Task<double> CalculateProductPriceAsync(string productId);

        Task<List<ProductWithPriceResponse>> GetAllProductsAndPricesAsync();

        Task<ProductWithPriceResponse> GetProductAndPriceByIdAsync(string productId);

        public TblProduct GetProduct(string id);
        public List<TblProduct> filterProductsByCategoryID(string categoryID);
        public List<TblProduct> GetProductsByName(string name);
        public List<TblProduct> GetAllProducts();
    }
}
