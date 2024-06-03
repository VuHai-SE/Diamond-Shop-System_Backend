using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOS;
using Services;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblGemPriceListsController : ControllerBase
    {
        private readonly ITblGemPriceListService iTblGemPriceListService;

        public TblGemPriceListsController()
        {
            iTblGemPriceListService = new TblGemPriceListService();
        }

        // GET: api/TblGemPriceLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMaterialCategory>>> GetTblGemPriceLists()
        {
            if (iTblGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }
            return iTblGemPriceListService.GetGemPriceLists().ToList();
        }

        // GET: api/TblGemPriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMaterialCategory>> GetTblGemPriceList(int id)
        {
            if (iTblGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }
            var tblGemPriceList = iTblGemPriceListService.GetTblGemPriceList(id);

            if (tblGemPriceList == null)
            {
                return NotFound();
            }

            return tblGemPriceList;
        }

        // PUT: api/TblGemPriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGemPriceList(int id, TblMaterialCategory tblGemPriceList)
        {
            if (id != tblGemPriceList.Id)
            {
                return BadRequest();
            }

            var isUpdate = iTblGemPriceListService.UpdateTblGemPriceList(id, tblGemPriceList);

            return NoContent();
        }

        // POST: api/TblGemPriceLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMaterialCategory>> PostTblGemPriceList(TblMaterialCategory tblGemPriceList)
        {
            var isAdd = iTblGemPriceListService.AddTblGemPriceList(tblGemPriceList);

            return CreatedAtAction("GetTblGemPriceList", new { id = tblGemPriceList.Id }, tblGemPriceList);
        }

        // DELETE: api/TblGemPriceLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGemPriceList(int id)
        {
            if (iTblGemPriceListService.GetGemPriceLists() == null)
            {
                return NotFound();
            }

            var isDelete = iTblGemPriceListService.DeleteTblGemPriceList(id);

            return NoContent();
        }

        //private bool TblGemPriceListExists(int id)
        //{
        //    return iTblGemPriceListService.TblGemPriceLists.Any(e => e.Id == id);
        //}
    }
}
