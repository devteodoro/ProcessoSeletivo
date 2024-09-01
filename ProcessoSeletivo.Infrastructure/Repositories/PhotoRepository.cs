using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using ProcessoSeletivo.Infrastructure.Data;

namespace ProcessoSeletivo.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly AppDataContext _dbContext;

        public PhotoRepository(AppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Photo> GetCurrentPhoto(int UserId) => await _dbContext.Photos.Where(p => p.PersonId == UserId && p.Current == true).FirstOrDefaultAsync();

        public async Task<List<Photo>> GetPhotosByUserId(int UserId) => await _dbContext.Photos.Where(p => p.PersonId == UserId).ToListAsync();

        public async Task<Photo> Create(Photo photo)
        {
            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
            return photo;
        }

        public async Task<Photo> Update(Photo photo)
        {
            _dbContext.Photos.Update(photo);
            await _dbContext.SaveChangesAsync();
            return photo;
        }
    }
}
