using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts
{
    public class DbLoja : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Produto>()
                .Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder c)
        {
            c.UseInMemoryDatabase("MinhaLoja");
        }
    }
}