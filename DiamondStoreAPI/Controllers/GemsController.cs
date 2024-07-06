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
        [Authorize(Roles = "Customer")]

        public async Task<ActionResult<IEnumerable<TblGem>>> GetTblGems()
        {
            if (iGemService.GetGems() == null)
            {
                return NotFound();
            }
            return iGemService.GetGems().ToList();
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

        // POST: api/Gems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblGem>> PostTblGem(TblGem tblGem)
        {
            var newGem = iGemService.AddGem(tblGem);

            return CreatedAtAction("GetTblGem", new { id = tblGem.GemId }, tblGem);
        }
    }
}
