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
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.Request;
using Services.Implement;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductsController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProductsAndPrices([FromQuery] ProductFilterCriteria criteria)
        {
            var productWithPriceList = await _productService.FilterProducts(criteria);
            if (productWithPriceList.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(productWithPriceList);
        }

        [HttpPost("CreateProduct")]
        //[Authorize(Roles = "Admin,Manager")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productService.CreateProductAsync(request);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] CreateProductRequest request)
        {
            var result = await _productService.UpdateProductAsync(id, request);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] TblProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _productService.UpdateProductAsync(id, product);
            if (!updateResult)
            {
                return NotFound();
            }

            return Ok("Product updated successfully.");
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductById(string id)
        //{
        //    var product = await _productService.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

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

        // GET: api/Products/CategoryName
        [HttpGet("Category/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            var category = _productCategoryService.GetCategoryByName(categoryName);
            var pruductList = await _productService.filterProductsByCategoryID(category.CategoryId);

            if (pruductList == null)
            {
                return NotFound();
            }
            return Ok(pruductList);
        }

        [HttpGet("ProductName/{name}")]
        public async Task<IActionResult> GetProductsByName(string name)
        {

            var pruductList = await _productService.GetProductsByName(name);

            if (pruductList == null)
            {
                return NotFound();
            }
            return Ok(pruductList);
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateProductStatus(List<string> productIdList)
        {
            foreach (var id in productIdList)
            {
                var isUpdate = await _productService.UpdateProductStatus(id);
                if (isUpdate == false) return NotFound("Product " + id + " not found");
            }
            return Ok("Update successfully");
        }
    }
}