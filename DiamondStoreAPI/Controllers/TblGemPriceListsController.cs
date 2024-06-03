using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Services;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblGemPriceListsController : ControllerBase
    {
        private readonly IGemPriceListService iGemPriceListService;

        public TblGemPriceListsController()
        {
            iGemPriceListService = new GemPriceListService();
        }

        // GET: api/TblGemPriceLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGemPriceList>>> GetTblGemPriceLists()
        {
            if (iGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }
            return iGemPriceListService.GetGemPriceLists().ToList();
        }

        // GET: api/TblGemPriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGemPriceList>> GetTblGemPriceList(int id)
        {
            if (iGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }

            var tblGemPriceList = iGemPriceListService.GetGemPriceList(id);

            if (tblGemPriceList == null)
            {
                return NotFound();
            }

            return tblGemPriceList;
        }

        // PUT: api/TblGemPriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGemPriceList(int id, TblGemPriceList tblGemPriceList)
        {
            if (id != tblGemPriceList.Id)
            {
                return BadRequest();
            }

            var isUpdate = iGemPriceListService.UpdateGemPriceList(id, tblGemPriceList);

            return NoContent();
        }

        // POST: api/TblGemPriceLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGemPriceList>> PostTblGemPriceList(TblGemPriceList tblGemPriceList)
        {
            if (iGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }

            var newGemPriceList = iGemPriceListService.AddGemPriceList(tblGemPriceList);

            return CreatedAtAction("GetTblGemPriceList", new { id = tblGemPriceList.Id }, tblGemPriceList);
        }

        // DELETE: api/TblGemPriceLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGemPriceList(int id)
        {
            if (iGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }

            var isDelete = iGemPriceListService.DeleteGemPriceList(id);
            
            return NoContent();
        }

        //private bool TblGemPriceListExists(int id)
        //{
        //    return iGemPriceListService.TblGemPriceLists.Any(e => e.Id == id);
        //}
    }
}
