using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class RoleRepository:IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Role>> GetAllAsync()
        {

            return await _dbContext.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();

        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _dbContext.Roles.Update(role);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);

            if (role == null)
                return;

            _dbContext.Roles.Remove(role);

            await _dbContext.SaveChangesAsync();
        }
    }
}
