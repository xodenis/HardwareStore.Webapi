using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HardwareStore.Webapi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _orderService.Get(UserId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OrderShortDto order)
        {
            try
            {
                var result = await _orderService.Add(UserId, order);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
