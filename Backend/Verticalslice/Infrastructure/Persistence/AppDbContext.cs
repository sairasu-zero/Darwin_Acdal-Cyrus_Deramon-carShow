using CarShowRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CarShowRegistration.Infrastructure.Persistence;
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Registration> Registrations => Set<Registration>();
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
