using CarShowRegistration.Domain.Entities;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;
public sealed record Query(RegistrationType? RegistrationType, RegistrationStatus? RegistrationStatus);
