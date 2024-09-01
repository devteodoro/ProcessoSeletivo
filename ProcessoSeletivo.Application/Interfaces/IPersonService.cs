using ProcessoSeletivo.Application.DTO;
using ProcessoSeletivo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessoSeletivo.Application.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDTO>> ListPeople(string? name, string? cpf, DateTime? dateOfBirth, Gender? sex);

        Task<PersonDTO> GetPersonById(int Id);

        Task<PersonDTO> AddPerson(PersonDTO Id);

        Task<PersonDTO> UpdatePerson(PersonDTO Id);

        Task<PersonDTO> DeletePerson(int Id);
    }
}
