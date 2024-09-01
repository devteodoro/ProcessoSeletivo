using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(string? name, string? cpf, DateTime? dateOfbirth, Gender? sex);

        Task<Person> GetByIdAsync(int PersonId);

        Task<Person> GetByCPFAsync(string cpf);

        Task<Person> CreateAsync(Person person);

        Task<Person> UpdateAsync(Person person);

        Task<Person> DeleteAsync(Person person);
    }
}
