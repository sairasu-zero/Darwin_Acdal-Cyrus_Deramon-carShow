using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CarShowRegistration.Infrastructure.Persistence;
public sealed class RegistrationRepository(AppDbContext context) : IRegistrationRepository
{
    public Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => context.Registrations.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    public async Task<IReadOnlyList<Registration>> GetAsync(RegistrationType? type, RegistrationStatus? status, CancellationToken cancellationToken = default)
    { var q = context.Registrations.AsNoTracking(); if (type.HasValue) q = q.Where(x => x.RegistrationType == type); if (status.HasValue) q = q.Where(x => x.Status == status); return await q.OrderByDescending(x => x.RegistrationDate).ToListAsync(cancellationToken); }
    public async Task<IReadOnlyList<Registration>> GetAllAsync(CancellationToken cancellationToken = default) => await context.Registrations.AsNoTracking().ToListAsync(cancellationToken);
    public async Task AddAsync(Registration registration, CancellationToken cancellationToken = default) { context.Registrations.Add(registration); await context.SaveChangesAsync(cancellationToken); }
}
