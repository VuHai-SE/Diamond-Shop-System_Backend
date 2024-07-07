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
using BusinessObjects.RequestModels;

namespace DiamondStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GemsController : ControllerBase
    {
        private readonly IGemService _gemService;

        public GemsController(IGemService gemService)
        {
            _gemService = gemService;
        }

        // GET: api/Gems
        [HttpGet]
        [Authorize(Roles = "Customer")]

        public async Task<ActionResult<IEnumerable<TblGem>>> GetTblGems()
        {
            if (_gemService.GetGems() == null)
            {
                return NotFound();
            }
            return _gemService.GetGems().ToList();
        }

        // GET: api/Gems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGem>> GetTblGem(string id)
        {
            if (_gemService.GetGems() == null)
            {
                return NotFound();
            }

            var tblGem = _gemService.GetGem(id);

            if (tblGem == null)
            {
                return NotFound();
            }

            return tblGem;
        }

        [HttpPost]
        public async Task<ActionResult<TblGem>> AddGem(AddGemRequest addGemRequest)
        {
            var gem = new TblGem
            {
                GemId = addGemRequest.GemId,
                GemName = addGemRequest.GemName,
                Polish = addGemRequest.Polish,
                Symmetry = addGemRequest.Symmetry,
                Fluorescence = addGemRequest.Fluorescence,
                Origin = addGemRequest.Origin,
                CaratWeight = addGemRequest.CaratWeight,
                Color = addGemRequest.Color,
                Cut = addGemRequest.Cut,
                Clarity = addGemRequest.Clarity,
                Shape = addGemRequest.Shape
            };

            var addedGem = _gemService.AddGem(gem);

            var report = new TblDiamondGradingReport
            {
                GemId = addedGem.GemId,
                GenerateDate = addGemRequest.GenerateDate,
                Image = addGemRequest.Image
            };

            _gemService.AddDiamondGradingReport(report);

            return CreatedAtAction(nameof(AddGem), new { id = addedGem.GemId }, addedGem);
        }
    }
}
