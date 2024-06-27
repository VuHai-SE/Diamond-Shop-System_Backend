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
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.Response;

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
        public async Task<ActionResult<IEnumerable<MaterialResponse>>> GetMaterialList()
        {
            var materialList = iMaterialPriceListService.GetMaterialList();
            if (materialList.IsNullOrEmpty())
            {
                return NotFound();
            }
            return materialList;
        }

        // GET: api/MaterialPriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialResponse>> GetMaterial(string id)
        {
            if (iMaterialPriceListService.GetMaterialList().IsNullOrEmpty())
            {
                return NotFound();
            }
            var material = iMaterialPriceListService.GetMaterial(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // POST: api/MaterialPriceLists
        [HttpPost]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialRequest request)
        {
            var result = await iMaterialPriceListService.CreateMaterialAsync(request);

            if (result == "Material ID already exists." || result == "Material Name already exists." || result.Contains("Effective date must be within"))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // PUT: api/MaterialPriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateMaterialPrice([FromBody] UpdateMeterialRequest request)
        {
            var materialPrice = iMaterialPriceListService.GetMaterialPriceByMaterialID(request.MaterialID);
            if (materialPrice == null)
            {
                return NotFound();
            }
            
            materialPrice.UnitPrice = request.NewPrice;
            materialPrice.EffDate = request.EffectDate;
            var isUpdate = iMaterialPriceListService.UpdateMaterialPriceList(materialPrice.Id, materialPrice);
            var productMaterialList = iProductMaterialService.GetProductMaterialByMaterialID(materialPrice.MaterialId);
            foreach ( var pm in productMaterialList )
            {
                await iProductService.UpdateMaterialPriceAndUnitPriceSize(pm.ProductId, materialPrice);
            }
            return Ok();
        }
    }
}
