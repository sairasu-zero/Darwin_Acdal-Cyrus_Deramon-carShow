using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CarShowRegistration.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CarShowDatabase")
            ?? throw new InvalidOperationException("Connection string 'CarShowDatabase' is not configured.");
        services.AddDbContext<AppDbContext>(o => o.UseSqlite(connectionString));
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        return services;
    }
}
