using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        private readonly IPhotoRepository _photoRepository;

        public PersonService(IPersonRepository personRepository, IPhotoRepository photoRepository)
        {
            _personRepository = personRepository;
            _photoRepository = photoRepository;
        }

        public async Task<PersonDTO> GetPersonById(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("O id da pessoa é inválido!");

            Person person = await _personRepository.GetById(Id);

            if (person == null)
                throw new Exception("Usuário não encontrado!");

            return new PersonDTO(person.Id, person.Name, person.LastName, person.CPF, person.DateOfBirth, person.Sex);
        }

        public async Task<List<PersonDTO>> ListPeople(string? name, string? cpf, DateTime? dateOfBitrh, Gender? sex)
        {
            List<Person> people = await _personRepository.List(name, cpf, dateOfBitrh, sex);

            List<PersonDTO> listPersonDTO = new List<PersonDTO>();

            if (people != null && people.Count > 0)
            {
                foreach (Person person in people)
                {
                    listPersonDTO.Add(new PersonDTO(person.Id, person.Name, person.LastName, person.CPF, person.DateOfBirth, person.Sex));
                }
            }

            return listPersonDTO;
        }

        public async Task<PersonDTO> AddPerson(PersonDTO personDTO)
        {
            Person response = await _personRepository.Create(new Person(personDTO.Name, personDTO.LastName, personDTO.CPF, personDTO.DateOfBirth, personDTO.Sex, personDTO.Photo));
            return new PersonDTO(response.Id, response.Name, response.LastName, response.CPF, response.DateOfBirth, response.Sex);
        }

        public async Task<PersonDTO> UpdatePerson(PersonDTO personDTO)
        {
            Person person = await _personRepository.GetById(personDTO.Id);

            if (person == null)
                throw new Exception("Usuário não encontrado!");

            person.Name = personDTO.Name;
            person.LastName = personDTO.LastName;
            person.CPF = personDTO.CPF;
            person.DateOfBirth = personDTO.DateOfBirth;
            person.Sex = personDTO.Sex;

            if (!string.IsNullOrEmpty(personDTO.Photo) && person.Photos.Count > 0)
            {
                Photo currentPhoto = person.Photos.FirstOrDefault(x => x.Current == true);

                if (currentPhoto != null)
                    currentPhoto.Current = false;

                person.Photos.Add(new Photo(personDTO.Photo, true, person.Id));
            }

            await _personRepository.Update(person);
            return personDTO;
        }

        public async Task<PersonDTO> DeletePerson(int Id)
        {
            Person person = await _personRepository.GetById(Id);

            if (person == null)
                throw new Exception("Pessoa não encontrada!");

            await _personRepository.Delete(person);
            return new PersonDTO(person.Id, person.Name, person.LastName, person.CPF, person.DateOfBirth, person.Sex);
        }
    }
}
