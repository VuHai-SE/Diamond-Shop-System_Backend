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
    [Route("api/[controller]")]
    [ApiController]
    public class GemsController : ControllerBase
    {
        private readonly IGemService iGemService;

        public GemsController(IGemService gemService)
        {
            iGemService = gemService;
        }

        // GET: api/Gems
        [HttpGet]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult<IEnumerable<TblGem>>> GetTblGems()
        {
            if (iGemService.GetGems() == null)
            {
                return NotFound();
            }
            return iGemService.GetGems();
        }

        // GET: api/Gems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGem>> GetTblGem(string id)
        {
            if (iGemService.GetGems() == null)
            {
                return NotFound();
            }

            var tblGem = iGemService.GetGem(id);

            if (tblGem == null)
            {
                return NotFound();
            }

            return tblGem;
        }

        // PUT: api/Gems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblGsem(string id, TblGem tblGem)
        //{
        //    if (id != tblGem.GemId)
        //    {
        //        return BadRequest();
        //    }

        //    iGemService.Entry(tblGem).State = EntityState.Modified;

        //    try
        //    {
        //        await iGemService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblGemExists(id))
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

        // POST: api/Gems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGem>> PostTblGem(TblGem tblGem)
        {
            var newGem = iGemService.AddGem(tblGem);

            return CreatedAtAction("GetTblGem", new { id = tblGem.GemId }, tblGem);
        }

        // DELETE: api/Gems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblGem(string id)
        //{
        //    var tblGem = await iGemService.TblGems.FindAsync(id);
        //    if (tblGem == null)
        //    {
        //        return NotFound();
        //    }

        //    iGemService.TblGems.Remove(tblGem);
        //    await iGemService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblGemExists(string id)
        //{
        //    return iGemService.TblGems.Any(e => e.GemId == id);
        //}
    }
}
