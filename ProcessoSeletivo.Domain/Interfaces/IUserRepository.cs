using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> List();

        Task<User> GetById(int UserId);

        Task<User> GetByName(string UserName);

        Task<User> Create(User user);

        Task<User> Update(User user);

        Task<User> Delete(User user);
    }
}
