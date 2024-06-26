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
using Services.DTOs.Response;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GemPriceListController : ControllerBase
    {
        private readonly IGemPriceListService iGemPriceListService;

        public GemPriceListController(IGemPriceListService gemPriceListService)
        {
            iGemPriceListService = gemPriceListService;
        }

        // GET: api/GemPriceList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GemPriceResponse>>> GetGemPriceLists()
        {
            if (iGemPriceListService.GetGemPriceLists() == null) {
                return NotFound();
            }
            return iGemPriceListService.GetGemPriceLists();
        }

        [HttpGet("FilterGemPriceList")]
        public async Task<ActionResult<IEnumerable<GemPriceResponse>>> FilterGemPriceList([FromQuery] GemPriceListFilterCriteria criteria)
        {
            var gemPriceList = iGemPriceListService.GetListByFourCAndOrigin(criteria);
            if (gemPriceList == null)
            {
                return NotFound("No result");
            }
            return gemPriceList;
        }
        // get: api/gempricelist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GemPriceResponse>> GetGemPrice(int id)
        {
            var gemPrice = iGemPriceListService.GetGemPrice(id);

            if (gemPrice == null)
            {
                return NotFound();
            }

            return Ok(gemPrice);
        }

        // PUT: api/GemPriceList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateGemPrice")]
        public async Task<IActionResult> UpdateGemPriceList([FromBody] UpdateGemPriceRequest request)
        {
            var isUpdate = iGemPriceListService.UpdateGemPriceList(request);
            if (isUpdate) return Ok();
            else return BadRequest();
        }

        // POST: api/GemPriceList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGemPriceList>> PostTblGemPriceList(TblGemPriceList tblGemPriceList)
        {
            TblGemPriceList newGemPirce = iGemPriceListService.AddGemPriceList(tblGemPriceList);
            return CreatedAtAction("GetTblGemPriceList", new { id = tblGemPriceList.Id }, tblGemPriceList);
        }

        // DELETE: api/GemPriceList/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblGemPriceList(int id)
        //{
        //    var tblGemPriceList = await iGemPriceListService.TblGemPriceLists.FindAsync(id);
        //    if (tblGemPriceList == null)
        //    {
        //        return NotFound();
        //    }

        //    iGemPriceListService.TblGemPriceLists.Remove(tblGemPriceList);
        //    await iGemPriceListService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblGemPriceListExists(int id)
        //{
        //    return iGemPriceListService.TblGemPriceLists.Any(e => e.Id == id);
        //}
    }
}
