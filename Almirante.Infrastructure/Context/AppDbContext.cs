using Almirante.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Almirante.Infrastructure.Context
{
    public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ObraDeArte>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(150);

            entity.Property(e => e.Autor)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(e => e.UrlImagem)
            .HasMaxLength(500);
        });
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<ObraDeArte> Obras { get; set; }
    }
}