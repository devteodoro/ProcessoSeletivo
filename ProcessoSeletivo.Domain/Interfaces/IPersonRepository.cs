using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> List(string? name, string? cpf, DateTime? dateOfbirth, Gender? sex);

        Task<Person> GetById(int PersonId);

        Task<Person> Create(Person person);

        Task<Person> Update(Person person);

        Task<Person> Delete(Person person);
    }
}
