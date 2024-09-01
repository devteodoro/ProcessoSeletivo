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

        public async Task<PersonDTO> GetPersonByIdAsync(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("O id da pessoa é inválido!");

            Person person = await _personRepository.GetByIdAsync(Id);

            if (person == null)
                throw new Exception("Usuário não encontrado!");

            return new PersonDTO(person.Id, person.Name, person.LastName, person.CPF, person.DateOfBirth, person.Sex);
        }

        public async Task<List<PersonDTO>> GetAllPersonAsync(string? name, string? cpf, DateTime? dateOfBitrh, Gender? sex)
        {
            List<Person> people = await _personRepository.GetAllAsync(name, cpf, dateOfBitrh, sex);

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

        public async Task<PersonDTO> AddPersonAsync(PersonDTO personDTO)
        {
            //verificar se o CPF já exite no banco
            Person person = await _personRepository.GetByCPFAsync(personDTO.CPF);

            if (person != null)
                throw new Exception($"Já existe um usuário com o CPF {personDTO.CPF} cadastrado!");

            Person response = await _personRepository.CreateAsync(new Person(personDTO.Name, personDTO.LastName, personDTO.CPF, personDTO.DateOfBirth, personDTO.Sex, personDTO.Photo));
            return new PersonDTO(response.Id, response.Name, response.LastName, response.CPF, response.DateOfBirth, response.Sex);
        }

        public async Task<PersonDTO> UpdatePersonAsync(PersonDTO personDTO)
        {
            Person personCPF = await _personRepository.GetByCPFAsync(personDTO.CPF);

            if (personCPF != null)
            {
                if(personDTO.Id != personCPF.Id)
                    throw new Exception($"Já existe um usuário com o CPF {personDTO.CPF} cadastrado!");
            }
                
            Person person = await _personRepository.GetByIdAsync(personDTO.Id);

            if (person == null)
                throw new Exception("Usuário não encontrado!");

            if (!string.IsNullOrEmpty(personDTO.Name))
                person.Name = personDTO.Name;

            if (!string.IsNullOrEmpty(personDTO.LastName))
                person.LastName = personDTO.LastName;

            if (!string.IsNullOrEmpty(personDTO.CPF))
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

            await _personRepository.UpdateAsync(person);
            return personDTO;
        }

        public async Task<PersonDTO> DeletePersonAsync(int Id)
        {
            Person person = await _personRepository.GetByIdAsync(Id);

            if (person == null)
                throw new Exception("Pessoa não encontrada!");

            await _personRepository.DeleteAsync(person);
            return new PersonDTO(person.Id, person.Name, person.LastName, person.CPF, person.DateOfBirth, person.Sex);
        }

        public bool ImadeSizeValid(long tamanho)
        {
            long MaxFileSize = 1 * 1024 * 1024;

            if(tamanho > MaxFileSize)
                return false;

            return true;
        }
    }
}
