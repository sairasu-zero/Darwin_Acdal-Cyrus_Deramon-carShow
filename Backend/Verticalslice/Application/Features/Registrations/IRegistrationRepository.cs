using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations;

public interface IRegistrationRepository
{
    Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Registration>> GetAsync(RegistrationType? type, RegistrationStatus? status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Registration>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Registration registration, CancellationToken cancellationToken = default);
}
