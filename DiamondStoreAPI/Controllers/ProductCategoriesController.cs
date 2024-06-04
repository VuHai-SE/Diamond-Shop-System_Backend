using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService iProductCategoryService;

        public ProductCategoriesController()
        {
            iProductCategoryService = new ProductCategoryService();
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

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[httpput("{id}")]
        //public async task<iactionresult> puttblproductcategory(string id, tblproductcategory tblproductcategory)
        //{
        //    if (id != tblproductcategory.categoryid)
        //    {
        //        return badrequest();
        //    }

        //    iproductcategoryservice.entry(tblproductcategory).state = entitystate.modified;

        //    try
        //    {
        //        await iproductcategoryservice.savechangesasync();
        //    }
        //    catch (dbupdateconcurrencyexception)
        //    {
        //        if (!tblproductcategoryexists(id))
        //        {
        //            return notfound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return nocontent();
        //}

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblProductCategory>> PostTblProductCategory(TblProductCategory tblProductCategory)
        {
            var newProductCategory = iProductCategoryService.AddProductCategories(tblProductCategory);
            
            return CreatedAtAction("GetTblProductCategory", new { id = tblProductCategory.CategoryId }, tblProductCategory);
        }

        // DELETE: api/ProductCategories/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteTblProductCategory(string id)
    //    {
    //        var tblProductCategory = await iProductCategoryService.TblProductCategories.FindAsync(id);
    //        if (tblProductCategory == null)
    //        {
    //            return NotFound();
    //        }

    //        iProductCategoryService.TblProductCategories.Remove(tblProductCategory);
    //        await iProductCategoryService.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool TblProductCategoryExists(string id)
    //    {
    //        return iProductCategoryService.TblProductCategories.Any(e => e.CategoryId == id);
    //    }
    }
}
