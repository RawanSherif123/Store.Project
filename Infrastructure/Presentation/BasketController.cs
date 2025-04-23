﻿using System;
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
    public class BasketController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id )
        {
          var result =  await servicesManager.BasketService.GetBasketAsync(id);
           return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
           var result = await   servicesManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
          await  servicesManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
