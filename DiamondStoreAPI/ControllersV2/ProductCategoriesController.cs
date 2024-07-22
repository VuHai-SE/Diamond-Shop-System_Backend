using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;
using Services.Implement;
using Microsoft.AspNetCore.Authorization;

namespace DiamondStoreAPI.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/ProductCategories/")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService iProductCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            iProductCategoryService = productCategoryService;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProductCategory>>> GetTblProductCategories()
        {
            if (iProductCategoryService.GetProductCategories() == null)
            {
                return NotFound();
            }
            return iProductCategoryService.GetProductCategories().ToList();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProductCategory>> GetTblProductCategory(string id)
        {
            if (iProductCategoryService.GetProductCategories() == null)
            {
                return NotFound();
            }

            var tblProductCategory = iProductCategoryService.GetProductCategory(id);

            if (tblProductCategory == null)
            {
                return NotFound();
            }

            return tblProductCategory;
        }

        // POST: api/ProductCategories
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<TblProductCategory>> PostTblProductCategory(TblProductCategory tblProductCategory)
        {
            var newProductCategory = iProductCategoryService.AddProductCategories(tblProductCategory);
            
            return CreatedAtAction("GetTblProductCategory", new { id = tblProductCategory.CategoryId }, tblProductCategory);
        }
    }
}
