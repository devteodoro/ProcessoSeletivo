using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Api.Filters;
using ProcessoSeletivo.Api.ViewModels.User;
using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("v1/user/list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var listUserDto = await _userService.GetAllUsersAsync();

                List<UserResultViewModel> listViewModel = new List<UserResultViewModel>();

                if (listUserDto != null)
                {
                    foreach (var userDto in listUserDto)
                    {
                        listViewModel.Add(new UserResultViewModel
                        {
                            Id = userDto.Id,
                            Name = userDto.Name,
                            Role = userDto.Role.ToString(),
                        });
                    }
                }

                return Ok(new ResultModel<List<UserResultViewModel>>(listViewModel));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [Authorize]
        [HttpGet("v1/user/{userId:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var userDto = await _userService.GetUserByIdAsync(userId);

                return Ok(new ResultModel<UserResultViewModel>(new UserResultViewModel()
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Role = userDto.Role.ToString(),
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpPost("v1/user/create")]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<UserResultViewModel>("Dados inválidos!"));

            try
            {
                UserDTO userDTO = new UserDTO(userViewModel.Name, userViewModel.Password, userViewModel.Role);

                UserDTO response = await _userService.AddUserAsync(userDTO);

                return Created(
                    $"v1/user/{response.Id}",
                    new ResultModel<UserResultViewModel>(new UserResultViewModel
                    {
                        Id = response.Id,
                        Name = response.Name,
                        Role = response.Role.ToString(),
                    }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpPut("v1/user/update")]
        public async Task<IActionResult> Update(UserUpdateViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<UserResultViewModel>("Dados inválidos!"));

            try
            {
                UserDTO userDTO = new UserDTO(userViewModel.Id, userViewModel.Name, userViewModel.Password, userViewModel.Role);

                UserDTO response = await _userService.UpdateUserAsync(userDTO);

                return Ok(new ResultModel<UserResultViewModel>(new UserResultViewModel
                {
                    Id = response.Id,
                    Name = response.Name,
                    Role = response.Role.ToString(),
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpDelete("v1/user/delete/{userId:int}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<UserResultViewModel>("Dados inválidos!"));

            try
            {
                UserDTO response = await _userService.DeleteUserAsync(userId);

                return Ok(new ResultModel<UserResultViewModel>(new UserResultViewModel
                {
                    Id = response.Id,
                    Name = response.Name,
                    Role = response.Role.ToString(),
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }
    }
}
