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
using Services.DTOs.Request;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialPriceListsController : ControllerBase
    {
        private readonly IMaterialPriceListService iMaterialPriceListService;
        private readonly IProductService iProductService;
        private readonly IProductMaterialService iProductMaterialService;

        public MaterialPriceListsController(IMaterialPriceListService materialPriceListService, IProductService productService, IProductMaterialService productMaterialService)
        {
            iMaterialPriceListService = materialPriceListService;
            iProductService = productService;
            iProductMaterialService = productMaterialService;
        }

        // GET: api/MaterialPriceLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMaterialPriceList>>> GetTblMaterialPriceLists()
        {
            if (iMaterialPriceListService.GetMaterialPriceLists() == null)
            {
                return NotFound();
            }
            return iMaterialPriceListService.GetMaterialPriceLists().ToList();
        }

        // GET: api/MaterialPriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMaterialPriceList>> GetTblMaterialPriceList(int id)
        {
            if (iMaterialPriceListService.GetMaterialPriceLists() == null)
            {
                return NotFound();
            }
            var tblMaterialPriceList = iMaterialPriceListService.GetMaterialPriceList(id);

            if (tblMaterialPriceList == null)
            {
                return NotFound();
            }

            return tblMaterialPriceList;
        }

        // PUT: api/MaterialPriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateMaterialPrice(int id, [FromBody] UpdateMeterialRequest request)
        {
            var materialPrice = iMaterialPriceListService.GetMaterialPriceList(id);
            if (materialPrice == null)
            {
                return NotFound();
            }
            
            materialPrice.UnitPrice = request.NewPrice;
            materialPrice.EffDate = request.EffectDate;
            var isUpdate = iMaterialPriceListService.UpdateMaterialPriceList(id, materialPrice);
            var productMaterialList = iProductMaterialService.GetProductMaterialByMaterialID(materialPrice.MaterialId);
            foreach ( var pm in productMaterialList )
            {
                iProductService.UpdateMaterialPriceAndUnitPriceSize(pm.ProductId, materialPrice);
            }
            return Ok();
        }

        // POST: api/MaterialPriceLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMaterialPriceList>> PostTblMaterialPriceList(TblMaterialPriceList tblMaterialPriceList)
        {
            if (iMaterialPriceListService.GetMaterialPriceLists() == null)
            {
                return NotFound();
            }
            
            var newMaterialPriceList = iMaterialPriceListService.AddMaterialPriceList(tblMaterialPriceList);
            return CreatedAtAction("GetTblMaterialPriceList", new { id = tblMaterialPriceList.Id }, tblMaterialPriceList);
        }

        // DELETE: api/MaterialPriceLists/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblMaterialPriceList(int id)
        //{
        //    if (iMaterialPriceListService.GetMaterialPriceLists() == null)
        //    {
        //        return NotFound();
        //    }
        //    var isDelete = iMaterialPriceListService.DeleteMaterialPriceList(id);

        //    return NoContent();
        //}

        //private bool TblMaterialPriceListExists(int id)
        //{
        //    return iMaterialPriceListService.TblMaterialPriceLists.Any(e => e.Id == id);
        //}
    }
}
