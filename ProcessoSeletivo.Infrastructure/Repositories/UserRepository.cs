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

        public async Task<List<User>> List() => await _dbContext.Users.ToListAsync();

        public async Task<User> GetById(int UserId) => await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == UserId);

        public async Task<User> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByName(string UserName) => await _dbContext.Users.FirstOrDefaultAsync(p => p.Name == UserName);
    }
}
