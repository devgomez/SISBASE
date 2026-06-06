using Microsoft.EntityFrameworkCore;
using SISBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();


    }


}
