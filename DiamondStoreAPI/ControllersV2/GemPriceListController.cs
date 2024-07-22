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
using Microsoft.AspNetCore.Authorization;

namespace DiamondStoreAPI.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/GemPriceList/")]
    [ApiController]
    public class GemPriceListsController : ControllerBase
    {
        private readonly IGemPriceListService iGemPriceListService;

        public GemPriceListsController(IGemPriceListService gemPriceListService)
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

        [Authorize(Roles = "Manager")]
        [HttpPut("UpdateGemPrice")]
        public async Task<IActionResult> UpdateGemPriceList([FromBody] UpdateGemPriceRequest request)
        {
            var isUpdate = iGemPriceListService.UpdateGemPriceList(request);
            if (isUpdate) return Ok();
            else return BadRequest();
        }

        // POST: api/GemPriceList
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<TblGemPriceList>> PostTblGemPriceList(TblGemPriceList tblGemPriceList)
        {
            TblGemPriceList newGemPirce = iGemPriceListService.AddGemPriceList(tblGemPriceList);
            return CreatedAtAction("GetTblGemPriceList", new { id = tblGemPriceList.Id }, tblGemPriceList);
        }
    }
}
