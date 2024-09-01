using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int UserId);

        Task<User> GetByNameAsync(string UserName);

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<User> DeleteAsync(User user);
    }
}
