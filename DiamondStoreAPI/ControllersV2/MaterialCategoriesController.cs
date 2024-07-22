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
    [Route("api/v2/MaterialCategories/")]
    [ApiController]
    public class MaterialCategoryController : ControllerBase
    {
        private readonly IMaterialCategoryService iMaterialCategoryService;

        public MaterialCategoryController(IMaterialCategoryService materialCategoryService)
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

        // POST: api/MaterialCategories
        [Authorize(Roles = "Manager")]
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
    }
}
