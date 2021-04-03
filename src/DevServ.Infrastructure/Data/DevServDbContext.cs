using DevServ.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevServ.Infrastructure
{
    public class DevServDbContext : DbContext
    {
        public DevServDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }

        public DbSet<Skill> Skills { get; set; }
    }
}
