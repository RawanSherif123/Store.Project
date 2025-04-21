using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts( [FromQuery] ProductSepcificationsParamters specParams)
        {
            var result = await servicesManager.productService.GetAllProductAsync(specParams);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id )
        {
           var result = await servicesManager.productService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await servicesManager.productService.GetProductBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await servicesManager.productService.GetProductTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
