using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Interfaces;
using ProcessoSeletivo.Domain.Models;
using ProcessoSeletivo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Design;

namespace ProcessoSeletivo.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDataContext _dbContext;

        public PersonRepository(AppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Person> GetByIdAsync(int personId) => await _dbContext.People.Include(x => x.Photos).FirstOrDefaultAsync(p => p.Id == personId);

        public async Task<Person> CreateAsync(Person person)
        {
            await _dbContext.People.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            _dbContext.People.Update(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<Person> DeleteAsync(Person person)
        {
            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> GetAllAsync(string? name, string? cpf, DateTime? dateOfbirth, Gender? sex)
        {
            var query = _dbContext.People.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.ToLower() == name.ToLower());
            
            if (!string.IsNullOrEmpty(cpf))
                query = query.Where(p => p.CPF == cpf);
                
            if (dateOfbirth != null)
                query = query.Where(p => p.DateOfBirth == dateOfbirth.Value);

            if (sex != null)
                query = query.Where(p => (int)p.Sex == (int)sex);

            return await query.ToListAsync();
        }

        public async Task<Person> GetByCPFAsync(string cpf) => await _dbContext.People.FirstOrDefaultAsync(p => p.CPF == cpf);
    }
}
