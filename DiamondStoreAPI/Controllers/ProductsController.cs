using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Services.DTOs.Response;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAndPrices()
        {
            var productsAndPrices = await _productService.GetAllProductsAndPricesAsync();
            var result = productsAndPrices.Select(pp => new ProductWithPriceResponse
            {
                product = pp.product,
                price = pp.price
            }).ToList();

            var response = new { products = result };
            return Ok(response);
        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductPrice(string productId)
        {
            var response = await _productService.GetProductAndPriceByIdAsync(productId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //[HttpGet("{productId}/price")]
        //public async Task<IActionResult> GetProductPrice(string productId)
        //{
        //    var price = await _productService.CalculateProductPriceAsync(productId);
        //    return Ok(price);
        //}
    }
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ProductsController : ControllerBase
    //{
    //    private readonly IProductService iProductService;

    //    public ProductsController()
    //    {
    //        iProductService = new ProductService();
    //    }

    //    // GET: api/Products
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<TblProduct>>> GetTblProducts()
    //    {
    //        if (iProductService.GetProducts() == null)
    //        {
    //            return NotFound();
    //        }
    //        return iProductService.GetProducts().ToList();
    //    }

    //    // GET: api/Products/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<TblProduct>> GetTblProduct(string id)
    //    {
    //        if (iProductService.GetProducts() == null)
    //        {
    //            return NotFound();
    //        }
    //        var tblProduct = iProductService.GetProduct(id);

    //        if (tblProduct == null)
    //        {
    //            return NotFound();
    //        }

    //        return tblProduct;
    //    }

    //    // PUT: api/Products/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    //[HttpPut("{id}")]
    //    //public async Task<IActionResult> PutTblProduct(string id, TblProduct tblProduct)
    //    //{
    //    //    if (id != tblProduct.ProductId)
    //    //    {
    //    //        return BadRequest();
    //    //    }

    //    //    iProductService.Entry(tblProduct).State = EntityState.Modified;

    //    //    try
    //    //    {
    //    //        await iProductService.SaveChangesAsync();
    //    //    }
    //    //    catch (DbUpdateConcurrencyException)
    //    //    {
    //    //        if (!TblProductExists(id))
    //    //        {
    //    //            return NotFound();
    //    //        }
    //    //        else
    //    //        {
    //    //            throw;
    //    //        }
    //    //    }

    //    //    return NoContent();
    //    //}

    //    // POST: api/Products
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<TblProduct>> PostTblProduct(TblProduct tblProduct)
    //    {
    //        var newProduct = iProductService.AddProduct(tblProduct);
    //        return CreatedAtAction("GetTblProduct", new { id = tblProduct.ProductId }, tblProduct);
    //    }

    //    // DELETE: api/Products/5
    //    //[HttpDelete("{id}")]
    //    //public async Task<IActionResult> DeleteTblProduct(string id)
    //    //{
    //    //    var tblProduct = await iProductService.TblProducts.FindAsync(id);
    //    //    if (tblProduct == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    iProductService.TblProducts.Remove(tblProduct);
    //    //    await iProductService.SaveChangesAsync();

    //    //    return NoContent();
    //    //}

    //    //private bool TblProductExists(string id)
    //    //{
    //    //    return iProductService.TblProducts.Any(e => e.ProductId == id);
    //    //}
    //}
}
