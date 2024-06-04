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
    public class MaterialPriceListsController : ControllerBase
    {
        private readonly IMaterialPriceListService iMaterialPriceListService;

        public MaterialPriceListsController(DiamondStoreContext context)
        {
            iMaterialPriceListService = new MaterialPriceListService();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMaterialPriceList(int id, TblMaterialPriceList tblMaterialPriceList)
        {
            if (id != tblMaterialPriceList.Id)
            {
                return BadRequest();
            }

            var isUpdate = iMaterialPriceListService.UpdateMaterialPriceList(id, tblMaterialPriceList);


            return NoContent();
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMaterialPriceList(int id)
        {
            if (iMaterialPriceListService.GetMaterialPriceLists() == null)
            {
                return NotFound();
            }
            var isDelete = iMaterialPriceListService.DeleteMaterialPriceList(id);

            return NoContent();
        }

        //private bool TblMaterialPriceListExists(int id)
        //{
        //    return iMaterialPriceListService.TblMaterialPriceLists.Any(e => e.Id == id);
        //}
    }
}
