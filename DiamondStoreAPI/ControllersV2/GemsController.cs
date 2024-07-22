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
using Microsoft.IdentityModel.Tokens;

namespace DiamondStoreAPI.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/Gems/")]
    [ApiController]
    public class GemController : ControllerBase
    {
        private readonly IGemService _gemService;

        public GemController(IGemService gemService)
        {
            _gemService = gemService;
        }

        // GET: api/Gems
        [HttpGet]
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

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult> AddGem(AddGemRequest addGemRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(addGemRequest.GemId) ||
                    string.IsNullOrEmpty(addGemRequest.GemName) ||
                    string.IsNullOrEmpty(addGemRequest.Polish) ||
                    string.IsNullOrEmpty(addGemRequest.Symmetry) ||
                    string.IsNullOrEmpty(addGemRequest.Fluorescence) ||
                    !addGemRequest.Origin.HasValue ||
                    !addGemRequest.CaratWeight.HasValue ||
                    string.IsNullOrEmpty(addGemRequest.Color) ||
                    string.IsNullOrEmpty(addGemRequest.Cut) ||
                    string.IsNullOrEmpty(addGemRequest.Clarity) ||
                    string.IsNullOrEmpty(addGemRequest.Shape) ||
                    !addGemRequest.GenerateDate.HasValue ||
                    string.IsNullOrEmpty(addGemRequest.Image))
                {
                    return BadRequest("All fields are required.");
                }

                var existingGem = _gemService.GetGem(addGemRequest.GemId);
                if (existingGem != null)
                {
                    return BadRequest("Gem with the same ID already exists.");
                }

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

                var response = new
                {
                    Message = "Gem added successfully",
                    Gem = addedGem
                };

                return CreatedAtAction(nameof(AddGem), new { id = addedGem.GemId }, response);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"A database error occurred: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{gemId}")]
        public async Task<ActionResult> DeleteGem(string gemId)
        {
            try
            {
                if (!_gemService.GemExists(gemId))
                {
                    return NotFound("Gem with the specified ID does not exist.");
                }

                if (_gemService.IsGemInProduct(gemId))
                {
                    return BadRequest("Gem is associated with a product and cannot be deleted.");
                }

                _gemService.DeleteDiamondGradingReport(gemId);
                _gemService.DeleteGem(gemId);

                return Ok("Gem deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{gemId}/gradingreport")]
        public async Task<ActionResult> UpdateDiamondGradingReport(string gemId, UpdateDiamondGradingReportRequest updateRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(gemId) ||
                    !updateRequest.GenerateDate.HasValue ||
                    string.IsNullOrEmpty(updateRequest.Image))
                {
                    return BadRequest("All fields are required.");
                }

                var existingReport = _gemService.GetDiamondGradingReportByGemId(gemId);
                if (existingReport == null)
                {
                    return NotFound("Diamond Grading Report with the specified Gem ID does not exist.");
                }

                existingReport.GenerateDate = updateRequest.GenerateDate;
                existingReport.Image = updateRequest.Image;

                _gemService.UpdateDiamondGradingReport(existingReport);

                return Ok("Diamond Grading Report updated successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"A database error occurred: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("GetDiamondGradingReport/{gemId}")]
        public async Task<ActionResult<string>> GetDiamondGradingReport(string gemId)
        {
            if (string.IsNullOrEmpty(gemId))
            {
                return BadRequest("GemId is required.");
            }

            if (!_gemService.GemExists(gemId))
            {
                return NotFound("No Diamond Grading Report found for the specified GemId.");
            }

            var report = _gemService.GetDiamondGradingReportByGemId(gemId);
            if (report != null)
            {
                return Ok(report.Image);
            }

            return NotFound("No data in database.");
        }
    }
}
