using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IProductService
    {
        public List<TblProduct> GetProducts();
        public TblProduct GetProduct(string id);
        public TblProduct AddProduct(TblProduct product);
        Task<double> CalculateProductPriceAsync(string productId);
    }
}
