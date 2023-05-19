using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Account;
using Services.Helpers.Responses;
using Services.Services.Interfaces;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto model)
        {
            return Ok(await _service.SignUpAsync(model));
        }
    }
}
