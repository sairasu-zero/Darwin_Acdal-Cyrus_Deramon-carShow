using CarShowRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CarShowRegistration.Infrastructure.Persistence.Seed;
public static class RegistrationSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Registrations.AnyAsync()) return;
        context.Registrations.AddRange(
            new Registration { Id=Guid.Parse("11111111-1111-1111-1111-111111111111"), RegistrationNumber="CS2027-001", RegistrationDate=DateTime.UtcNow, ParticipantName="Maria Santos", ContactNumber="09171234567", Email="maria.santos@example.com", Address="Sorsogon City", CarMakeModel="Toyota Supra", PlateNumber="CSV-2027-001", RegistrationType=RegistrationType.Exhibitor, Status=RegistrationStatus.Pending, RegistrationFee=3000m, AmountPaid=3000m },
            new Registration { Id=Guid.Parse("22222222-2222-2222-2222-222222222222"), RegistrationNumber="CS2027-002", RegistrationDate=DateTime.UtcNow, ParticipantName="Juan Dela Cruz", ContactNumber="09181234567", Email="juan.delacruz@example.com", Address="Sorsogon City", CarMakeModel="Honda Civic Type R", PlateNumber="CSV-2027-002", RegistrationType=RegistrationType.Competitor, Status=RegistrationStatus.Confirmed, RegistrationFee=5000m, AmountPaid=2500m });
        await context.SaveChangesAsync();
    }
}
