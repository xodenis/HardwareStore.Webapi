using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HardwareStore.Webapi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get_info")]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                var result = await _userService.GetInfo(UserId);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpPut("edit_info")]
        public async Task<IActionResult> EditInfo([FromBody] UserInfoDto info)
        {
            try
            {
                var result = await _userService.EditInfo(info, UserId);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
