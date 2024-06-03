using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiamondStoreAPI.Models;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly DiamondStoreContext _context;

        public ProductCategoriesController(DiamondStoreContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProductCategory>>> GetTblProductCategories()
        {
            return await _context.TblProductCategories.ToListAsync();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProductCategory>> GetTblProductCategory(string id)
        {
            var tblProductCategory = await _context.TblProductCategories.FindAsync(id);

            if (tblProductCategory == null)
            {
                return NotFound();
            }

            return tblProductCategory;
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblProductCategory(string id, TblProductCategory tblProductCategory)
        {
            if (id != tblProductCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(tblProductCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProductCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblProductCategory>> PostTblProductCategory(TblProductCategory tblProductCategory)
        {
            _context.TblProductCategories.Add(tblProductCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblProductCategoryExists(tblProductCategory.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblProductCategory", new { id = tblProductCategory.CategoryId }, tblProductCategory);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblProductCategory(string id)
        {
            var tblProductCategory = await _context.TblProductCategories.FindAsync(id);
            if (tblProductCategory == null)
            {
                return NotFound();
            }

            _context.TblProductCategories.Remove(tblProductCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblProductCategoryExists(string id)
        {
            return _context.TblProductCategories.Any(e => e.CategoryId == id);
        }
    }
}
