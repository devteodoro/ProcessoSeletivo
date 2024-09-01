using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Api.ViewModels;
using ProcessoSeletivo.Api.ViewModels.Auth;
using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Api.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
 

        [HttpPost("v1/auth")]
        public async Task<IActionResult> Auth(AuthViewModel authViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultModel<UserDTO>("Dados invalidos!"));

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultModel<string>($"Falha interna no servidor! {ex.Message}"));
            }      
        }
    }
}
