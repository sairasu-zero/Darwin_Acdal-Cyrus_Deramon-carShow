using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarShowRegistration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Features.Registrations.CreateRegistration.CreateRegistrationValidator>();
        services.AddScoped<Features.Registrations.CreateRegistration.CreateRegistrationHandler>();
        services.AddScoped<Features.Registrations.GetRegistrations.GetRegistrationsHandler>();
        services.AddScoped<Features.Registrations.GetRegistrationById.GetRegistrationByIdHandler>();
        services.AddScoped<Features.Registrations.GetRegistrationSummary.GetRegistrationSummaryHandler>();
        return services;
    }
}
