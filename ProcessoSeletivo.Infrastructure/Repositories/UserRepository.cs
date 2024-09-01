using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using ProcessoSeletivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ProcessoSeletivo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _dbContext;

        public UserRepository(AppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();

        public async Task<User> GetByIdAsync(int UserId) => await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == UserId);

        public async Task<User> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByNameAsync(string UserName) => await _dbContext.Users.FirstOrDefaultAsync(p => p.Name == UserName);
    }
}
