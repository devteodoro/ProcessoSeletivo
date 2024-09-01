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
        Task<List<PersonDTO>> GetAllPersonAsync(string? name, string? cpf, DateTime? dateOfBirth, Gender? sex);

        Task<PersonDTO> GetPersonByIdAsync(int Id);

        Task<PersonDTO> AddPersonAsync(PersonDTO Id);

        Task<PersonDTO> UpdatePersonAsync(PersonDTO Id);

        Task<PersonDTO> DeletePersonAsync(int Id);

        bool ImadeSizeValid(long tamanho);
    }
}
