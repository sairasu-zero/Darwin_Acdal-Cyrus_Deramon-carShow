using CarShowRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShowRegistration.Infrastructure.Persistence.Seed;

public static class RegistrationSeeder
{
    private static readonly SeedRegistration[] SeedRegistrations =
    [
        new("30000000-0000-0000-0000-000000000001", "Alyssa Mendoza", "09170000001", "alyssa.mendoza@example.com", "Sorsogon City, Sorsogon", "Toyota GR Supra", "SNG 2701", RegistrationType.Exhibitor, RegistrationStatus.Pending, 0m),
        new("30000000-0000-0000-0000-000000000002", "Rafael Villanueva", "09170000002", "rafael.villanueva@example.com", "Legazpi City, Albay", "Honda Civic Type R", "ALB 2702", RegistrationType.Competitor, RegistrationStatus.Confirmed, 1250m),
        new("30000000-0000-0000-0000-000000000003", "Bianca Reyes", "09170000003", "bianca.reyes@example.com", "Naga City, Camarines Sur", "Mazda RX-7 Spirit R", "CST 2703", RegistrationType.VIP, RegistrationStatus.CheckedIn, 5000m),
        new("30000000-0000-0000-0000-000000000004", "Miguel Santos", "09170000004", "miguel.santos@example.com", "Daraga, Albay", "Mitsubishi Lancer Evolution IX", "ALB 2704", RegistrationType.Competitor, RegistrationStatus.Pending, 500m),
        new("30000000-0000-0000-0000-000000000005", "Camille Garcia", "09170000005", "camille.garcia@example.com", "Tabaco City, Albay", "Nissan Skyline GT-R R34", "ALB 2705", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
        new("30000000-0000-0000-0000-000000000006", "Enzo Bautista", "09170000006", "enzo.bautista@example.com", "Pili, Camarines Sur", "Subaru WRX STI", "CST 2706", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
        new("30000000-0000-0000-0000-000000000007", "Patricia Cruz", "09170000007", "patricia.cruz@example.com", "Bulan, Sorsogon", "Porsche 911 Carrera S", "SNG 2707", RegistrationType.VIP, RegistrationStatus.Pending, 2000m),
        new("30000000-0000-0000-0000-000000000008", "Noah Fernandez", "09170000008", "noah.fernandez@example.com", "Iriga City, Camarines Sur", "Toyota AE86 Sprinter Trueno", "CST 2708", RegistrationType.Exhibitor, RegistrationStatus.Cancelled, 0m),
        new("30000000-0000-0000-0000-000000000009", "Sofia Ramos", "09170000009", "sofia.ramos@example.com", "Gubat, Sorsogon", "Honda NSX", "SNG 2709", RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
        new("30000000-0000-0000-0000-000000000010", "Gabriel Aquino", "09170000010", "gabriel.aquino@example.com", "Daet, Camarines Norte", "Ford Mustang GT", "CNO 2710", RegistrationType.VIP, RegistrationStatus.CheckedIn, 3000m),
        new("30000000-0000-0000-0000-000000000011", "Isabella Torres", "09170000011", "isabella.torres@example.com", "Panganiban, Catanduanes", "Nissan Silvia S15 Spec-R", "CAT 2711", RegistrationType.Exhibitor, RegistrationStatus.Pending, 750m),
        new("30000000-0000-0000-0000-000000000012", "Lorenzo Castillo", "09170000012", "lorenzo.castillo@example.com", "Masbate City, Masbate", "Chevrolet Camaro SS", "MAS 2712", RegistrationType.Competitor, RegistrationStatus.Cancelled, 1250m),
        new("30000000-0000-0000-0000-000000000013", "Andrea Flores", "09170000013", "andrea.flores@example.com", "Pili, Camarines Sur", "Volkswagen Golf GTI", "CST 2713", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m),
        new("30000000-0000-0000-0000-000000000014", "Diego Navarro", "09170000014", "diego.navarro@example.com", "Sorsogon City, Sorsogon", "Toyota Land Cruiser 70", "SNG 2714", RegistrationType.Exhibitor, RegistrationStatus.CheckedIn, 1500m),
        new("30000000-0000-0000-0000-000000000015", "Mikaela Lim", "09170000015", "mikaela.lim@example.com", "Legazpi City, Albay", "BMW M3 Competition", "ALB 2715", RegistrationType.Competitor, RegistrationStatus.Pending, 0m),
        new("30000000-0000-0000-0000-000000000016", "Nathaniel Perez", "09170000016", "nathaniel.perez@example.com", "Naga City, Camarines Sur", "Dodge Challenger R/T", "CST 2716", RegistrationType.VIP, RegistrationStatus.Cancelled, 0m),
        new("30000000-0000-0000-0000-000000000017", "Chloe Mercado", "09170000017", "chloe.mercado@example.com", "Donsol, Sorsogon", "Mitsubishi Montero Sport", "SNG 2717", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
        new("30000000-0000-0000-0000-000000000018", "Sebastian Ortega", "09170000018", "sebastian.ortega@example.com", "Virac, Catanduanes", "Mazda MX-5 RF", "CAT 2718", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 1000m),
        new("30000000-0000-0000-0000-000000000019", "Danica Flores", "09170000019", "danica.flores@example.com", "Masbate City, Masbate", "Hyundai Ioniq 5 N", "MAS 2719", RegistrationType.VIP, RegistrationStatus.Pending, 5000m),
        new("30000000-0000-0000-0000-000000000020", "Paolo Rivera", "09170000020", "paolo.rivera@example.com", "Pili, Camarines Sur", "Suzuki Jimny 5-Door", "CST 2720", RegistrationType.Exhibitor, RegistrationStatus.Cancelled, 500m),
        new("30000000-0000-0000-0000-000000000021", "Beatrice Santiago", "09170000021", "beatrice.santiago@example.com", "Daraga, Albay", "Lexus LC 500", "ALB 2721", RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
        new("30000000-0000-0000-0000-000000000022", "Andres Dominguez", "09170000022", "andres.dominguez@example.com", "Bulan, Sorsogon", "Isuzu D-Max X-Series", "SNG 2722", RegistrationType.VIP, RegistrationStatus.CheckedIn, 2500m),
        new("30000000-0000-0000-0000-000000000023", "Katrina Valdez", "09170000023", "katrina.valdez@example.com", "Daet, Camarines Norte", "Toyota GR Yaris", "CNO 2723", RegistrationType.Exhibitor, RegistrationStatus.Pending, 1500m),
        new("30000000-0000-0000-0000-000000000024", "Emilio Dizon", "09170000024", "emilio.dizon@example.com", "Naga City, Camarines Sur", "Nissan Patrol Safari", "CST 2724", RegistrationType.Competitor, RegistrationStatus.Cancelled, 2500m),
        new("30000000-0000-0000-0000-000000000025", "Juliana Co", "09170000025", "juliana.co@example.com", "Sorsogon City, Sorsogon", "Ferrari 488 GTB", "SNG 2725", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m)
    ];

    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        var seedIds = SeedRegistrations.Select(seed => Guid.Parse(seed.Id)).ToArray();
        var existingIds = await context.Registrations
            .Where(registration => seedIds.Contains(registration.Id))
            .Select(registration => registration.Id)
            .ToHashSetAsync(cancellationToken);
        var registrationsToAdd = SeedRegistrations
            .Where(seed => !existingIds.Contains(Guid.Parse(seed.Id)))
            .ToArray();

        if (registrationsToAdd.Length == 0)
            return;

        var existingNumbers = await context.Registrations
            .AsNoTracking()
            .Where(registration => registration.RegistrationNumber.StartsWith("CS27-"))
            .Select(registration => registration.RegistrationNumber)
            .ToListAsync(cancellationToken);
        var nextNumber = existingNumbers
            .Select(number => int.TryParse(number.AsSpan(5), out var value) ? value : 0)
            .DefaultIfEmpty(0)
            .Max() + 1;
        var registrationDate = new DateTime(2027, 2, 10, 8, 0, 0, DateTimeKind.Utc);

        foreach (var seed in registrationsToAdd)
        {
            var registrationType = seed.RegistrationType;
            var number = nextNumber++;
            context.Registrations.Add(new Registration
            {
                Id = Guid.Parse(seed.Id),
                RegistrationNumber = $"CS27-{number:D4}",
                RegistrationDate = registrationDate.AddMinutes(number),
                ParticipantName = seed.ParticipantName,
                ContactNumber = seed.ContactNumber,
                Email = seed.Email,
                Address = seed.Address,
                CarMakeModel = seed.CarMakeModel,
                PlateNumber = seed.PlateNumber,
                RegistrationType = registrationType,
                Status = seed.Status,
                RegistrationFee = Registration.FeeFor(registrationType),
                AmountPaid = seed.AmountPaid
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private sealed record SeedRegistration(
        string Id,
        string ParticipantName,
        string ContactNumber,
        string Email,
        string Address,
        string CarMakeModel,
        string PlateNumber,
        RegistrationType RegistrationType,
        RegistrationStatus Status,
        decimal AmountPaid);
}
