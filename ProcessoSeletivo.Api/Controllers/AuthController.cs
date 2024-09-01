using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using ProcessoSeletivo.Api.ViewModels;
using ProcessoSeletivo.Api.ViewModels.Auth;
using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("v1/auth")]
        public async Task<IActionResult> Auth(AuthViewModel authViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultModel<UserDTO>("Dados invalidos!"));

                string token = await _userService.Authenticate(new UserDTO() { Name = authViewModel.Name, Password = authViewModel.Password });

                if(string.IsNullOrEmpty(token))
                    return Unauthorized();

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultModel<string>($"Falha interna no servidor! {ex.Message}"));
            }      
        }
    }
}
