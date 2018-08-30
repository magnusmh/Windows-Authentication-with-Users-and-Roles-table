using AuthApp.Domain;
using AuthApp.Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthApp.Data
{
    public class AuthAppDbContext : DbContext
    {
        public AuthAppDbContext(DbContextOptions<AuthAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TestTable> TestTables { get; set; }
    }
    
}