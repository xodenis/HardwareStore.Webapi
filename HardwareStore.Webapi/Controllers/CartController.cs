using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HardwareStore.Webapi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _cartService.Get(UserId);
                return Created("", result);
            }
            catch(EmptyCartException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(int productId, int count)
        {
            try
            {
                var result = await _cartService.Add(UserId, productId, count);
                return Created("", result);
            }
            catch (ProductNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(int productId)
        {
            try
            {
                var result = await _cartService.Remove(UserId, productId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("remove_all")]
        public async Task<IActionResult> RemoveAll()
        {
            try
            {
                var result = await _cartService.RemoveRange(UserId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
