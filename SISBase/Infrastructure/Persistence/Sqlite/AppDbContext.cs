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

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Unit> Units => Set<Unit>();

        public DbSet<Option> Options => Set<Option>();

        public DbSet<Rol_Options> RoleOptions => Set<Rol_Options>();


    }


}
