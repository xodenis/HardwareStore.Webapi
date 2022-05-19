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
    [Route("favorites")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _favoritesService.Get(UserId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(int productId)
        {
            try
            {
                var result = await _favoritesService.Add(UserId, productId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(int productId)
        {
            try
            {
                var result = await _favoritesService.Remove(UserId, productId);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
