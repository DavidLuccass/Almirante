using Almirante.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Almirante.Infrastructure.Context
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<ObraDeArte> Obras { get; set; }
    }
}