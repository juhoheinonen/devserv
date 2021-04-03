using DevServ.Core.Entities;
using DevServ.Core.Exceptions;
using DevServ.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevServ.Infrastructure
{
    public class DeveloperRepository : IRepository<Developer>
    {
        private readonly DevServDbContext _dbContext;

        public DeveloperRepository(DevServDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Developer> GetByIdAsync(int id)
        {
            return await _dbContext.Developers.
                Where(d => !d.IsDeleted).
                Include(d => d.Skills).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Developer>> ListAsync()
        {
            return await _dbContext.Developers.
                Where(d => !d.IsDeleted).
                Include(d => d.Skills).ToListAsync();
        }

        public async Task UpdateAsync(Developer entity)
        {
            var existingItem = await GetByIdAsync(entity.Id);

            if (existingItem == null)
            {
                throw new NoEntityFoundWithIdException();
            }

            existingItem.FirstName = entity.FirstName;
            existingItem.LastName = entity.LastName;
            existingItem.Description = entity.Description;
            existingItem.Email = entity.Email;
            existingItem.PhoneNumber = entity.PhoneNumber;
            existingItem.SocialSecurityNumber = entity.SocialSecurityNumber;
            existingItem.HomePage = entity.HomePage;
            existingItem.OpenToWork = entity.OpenToWork;

            existingItem.Skills.Clear();

            existingItem.Skills = entity.Skills;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existingItem = await GetByIdAsync(id);

            if (existingItem == null)
            {
                throw new NoEntityFoundWithIdException();
            }

            existingItem.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Developer> AddAsync(Developer entity)
        {
            await _dbContext.Developers.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }        
    }
}
