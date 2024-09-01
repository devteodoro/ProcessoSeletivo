using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Api.Filters;
using ProcessoSeletivo.Api.Helpers;
using ProcessoSeletivo.Api.ViewModels;
using ProcessoSeletivo.Api.ViewModels.Person;
using ProcessoSeletivo.Api.ViewModels.User;
using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Application.Services;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using System.Net.Mime;

namespace ProcessoSeletivo.Api.Controllers
{

    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IPhotoService _photoService;

        public PersonController(IPersonService personService, IPhotoService photoService)
        {
            _personService = personService;
            _photoService = photoService;
        }

        [Authorize]
        [HttpGet("v1/person/list")]
        public async Task<IActionResult> List(string? name, string? cpf, DateTime? dateOfbirth, Gender? sex)
        {
            try
            {
                List<PersonResultViewModel> listPersonResult = new();

                List<PersonDTO> people = await _personService.GetAllPersonAsync(name, cpf, dateOfbirth, sex);

                if (people != null && people.Any())
                {
                    foreach (PersonDTO personDTO in people)
                    {
                        listPersonResult.Add(new PersonResultViewModel()
                        {
                            Id = personDTO.Id,
                            Name = personDTO.Name,
                            LastName = personDTO.LastName,
                            CPF = personDTO.CPF,
                            DateOfBirth = personDTO.DateOfBirth,
                            Sex = personDTO.Sex
                        });
                    }
                }

                return Ok(new ResultModel<List<PersonResultViewModel>>(listPersonResult));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<string>($"Falha interna no servidor! {e.Message}"));
            }
        }

        [Authorize]
        [HttpGet("v1/person/{PersonId:int}")]
        public async Task<IActionResult> Get(int PersonId)
        {
            try
            {
                var personDto = await _personService.GetPersonByIdAsync(PersonId);

                return Ok(new ResultModel<PersonResultViewModel>(new PersonResultViewModel()
                {
                    Id = personDto.Id,
                    Name = personDto.Name,
                    LastName = personDto.LastName,
                    CPF = personDto.CPF,
                    DateOfBirth = personDto.DateOfBirth,
                    Sex = personDto.Sex
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [Authorize]
        [HttpGet("v1/person/getphoto/{PersonId:int}")]
        public async Task<IActionResult> GetPhoto(int PersonId)
        {
            try
            {
                PhotoDTO photodto = await _photoService.GetCurrentPhotoByUserId(PersonId);
                byte[] fileBytes = Convert.FromBase64String(photodto.Image);
                return File(fileBytes, "image/jpeg");
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<Person>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpPost("v1/person/create")]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<PersonResultViewModel>("Dados inválidos!"));

            if (personViewModel.Photo != null)
            {
                if (!_personService.ImadeSizeValid(personViewModel.Photo.Length))
                    return BadRequest(new ResultModel<PersonResultViewModel>("O tamanho máximo da foto é 1MB!"));
            }

            try
            {
                string base64jpg = string.Empty;
                if (personViewModel.Photo != null)
                    base64jpg = ImageHelper.ConvertImageToBase64JPG(personViewModel.Photo);

                PersonDTO persondto = new PersonDTO(personViewModel.Name, personViewModel.LastName, personViewModel.CPF, personViewModel.DateOfBirth, personViewModel.Sex, base64jpg);

                PersonDTO response = await _personService.AddPersonAsync(persondto);

                return Created(
                    $"v1/person/{response.Id}",
                    new ResultModel<PersonResultViewModel>(new PersonResultViewModel
                    {
                        Id = response.Id,
                        Name = response.Name,
                        LastName = response.LastName,
                        CPF = response.CPF,
                        DateOfBirth = response.DateOfBirth,
                        Sex = response.Sex
                    }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpPut("v1/person/update")]
        public async Task<IActionResult> Update(PersonUpdateViewModel personViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<PersonResultViewModel>("Dados inválidos!"));

            if (personViewModel.Photo != null)
            {
                if (!_personService.ImadeSizeValid(personViewModel.Photo.Length))
                    return BadRequest(new ResultModel<PersonResultViewModel>("O tamanho máximo da foto é 1MB!"));
            }

            try
            {
                string base64jpg = string.Empty;
                if (personViewModel.Photo != null)
                    base64jpg = ImageHelper.ConvertImageToBase64JPG(personViewModel.Photo);

                PersonDTO persondto = new PersonDTO(personViewModel.Id, personViewModel.Name, personViewModel.LastName, personViewModel.CPF, personViewModel.DateOfBirth, personViewModel.Sex, base64jpg);

                PersonDTO response = await _personService.UpdatePersonAsync(persondto);

                return Ok(new ResultModel<PersonResultViewModel>(new PersonResultViewModel
                {
                    Id = response.Id,
                    Name = response.Name,
                    LastName = response.LastName,
                    CPF = response.CPF,
                    DateOfBirth = response.DateOfBirth,
                    Sex = response.Sex
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }

        [CustomAuthorize(Role.Admin)]
        [HttpDelete("v1/person/delete/{PersonId:int}")]
        public async Task<IActionResult> Delete(int PersonId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultModel<PersonResultViewModel>("Dados inválidos!"));

            try
            {
                PersonDTO response = await _personService.DeletePersonAsync(PersonId);

                return Ok(new ResultModel<PersonResultViewModel>(new PersonResultViewModel
                {
                    Id = response.Id,
                    Name = response.Name,
                    LastName = response.LastName,
                    CPF = response.CPF,
                    DateOfBirth = response.DateOfBirth,
                    Sex = response.Sex
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultModel<UserResultViewModel>($"Falha interna no servidor. {e.Message}"));
            }
        }
    }
}
