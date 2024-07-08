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

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialCategoriesController : ControllerBase
    {
        private readonly IMaterialCategoryService iMaterialCategoryService;

        public MaterialCategoriesController(IMaterialCategoryService materialCategoryService)
        {
            iMaterialCategoryService = materialCategoryService;
        }

        // GET: api/MaterialCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMaterialCategory>>> GetTblMaterialCategories()
        {
            if (iMaterialCategoryService.GetMaterialCategories() == null)
            {
                return NotFound();
            }
            return iMaterialCategoryService.GetMaterialCategories().ToList();
        }

        // GET: api/MaterialCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMaterialCategory>> GetTblMaterialCategory(string id)
        {
            if (iMaterialCategoryService.GetMaterialCategories() == null)
            {
                return NotFound();
            }
            var tblMaterialCategory = iMaterialCategoryService.GetMaterialCategory(id);

            if (tblMaterialCategory == null)
            {
                return NotFound();
            }

            return tblMaterialCategory;
        }

        // PUT: api/MaterialCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblMaterialCategory(string id, TblMaterialCategory tblMaterialCategory)
        //{
        //    if (id != tblMaterialCategory.MaterialId)
        //    {
        //        return BadRequest();
        //    }

        //    iMaterialCategoryService.Entry(tblMaterialCategory).State = EntityState.Modified;

        //    try
        //    {
        //        await iMaterialCategoryService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblMaterialCategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/MaterialCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMaterialCategory>> PostTblMaterialCategory(TblMaterialCategory tblMaterialCategory)
        {
            if (iMaterialCategoryService.GetMaterialCategories() == null)
            {
                return NotFound();
            }
            var newMaterialCategory = iMaterialCategoryService.AddMaterialCategory(tblMaterialCategory);
            
            return CreatedAtAction("GetTblMaterialCategory", new { id = tblMaterialCategory.MaterialId }, tblMaterialCategory);
        }

        // DELETE: api/MaterialCategories/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblMaterialCategory(string id)
        //{
        //    var tblMaterialCategory = await iMaterialCategoryService.TblMaterialCategories.FindAsync(id);
        //    if (tblMaterialCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    iMaterialCategoryService.TblMaterialCategories.Remove(tblMaterialCategory);
        //    await iMaterialCategoryService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblMaterialCategoryExists(string id)
        //{
        //    return iMaterialCategoryService.TblMaterialCategories.Any(e => e.MaterialId == id);
        //}
    }
}
