using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Petshop.Domain.Model;

namespace Petshop.Infra.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Animal> Animais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Animal>()
             .HasOne(a => a.Cliente)
             .WithMany(u => u.Animais)
             .HasForeignKey(a => a.ClienteId);

            modelBuilder.Entity<Animal>()
             .Property(s => s.Genero)
             .HasConversion<string>();

             modelBuilder.Entity<Animal>()
             .Property(s => s.AnimalCategoria)
             .HasConversion<string>();

        }
    }   
}