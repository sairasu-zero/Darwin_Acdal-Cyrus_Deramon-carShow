using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using CarShowRegistration.Api.Endpoints.Registrations;
using CarShowRegistration.Application;
using CarShowRegistration.Infrastructure;
using CarShowRegistration.Infrastructure.Persistence;
using CarShowRegistration.Infrastructure.Persistence.Seed;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

var databasePath = DatabaseLocation.GetDatabasePath(builder.Environment.ContentRootPath);
builder.Configuration["ConnectionStrings:CarShowDatabase"] = $"Data Source={databasePath}";

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.MapRegistrationEndpoints();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var connection = context.Database.GetDbConnection();
    await context.Database.OpenConnectionAsync();
    await using (var columnCommand = connection.CreateCommand())
    {
        columnCommand.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'Registrations';";
        var hasRegistrations = Convert.ToInt64(await columnCommand.ExecuteScalarAsync()) > 0;
        columnCommand.CommandText = "SELECT COUNT(*) FROM pragma_table_info('Registrations') WHERE name = 'RegistrationNumber';";
        var hasNewSchema = Convert.ToInt64(await columnCommand.ExecuteScalarAsync()) > 0;

        if (hasRegistrations && !hasNewSchema)
        {
            await using var migration = connection.CreateCommand();
            migration.CommandText = @"
BEGIN;
CREATE TABLE Registrations_New (
    Id TEXT NOT NULL CONSTRAINT PK_Registrations_New PRIMARY KEY,
    RegistrationNumber TEXT NOT NULL,
    RegistrationDate TEXT NOT NULL,
    ParticipantName TEXT NOT NULL,
    ContactNumber TEXT NOT NULL,
    Email TEXT NOT NULL,
    Address TEXT NOT NULL,
    CarMakeModel TEXT NOT NULL,
    PlateNumber TEXT NOT NULL,
    RegistrationType TEXT NOT NULL,
    Status TEXT NOT NULL,
    RegistrationFee TEXT NOT NULL,
    AmountPaid TEXT NOT NULL
);
INSERT INTO Registrations_New
(Id, RegistrationNumber, RegistrationDate, ParticipantName, ContactNumber, Email, Address, CarMakeModel, PlateNumber, RegistrationType, Status, RegistrationFee, AmountPaid)
SELECT Id,
       'CS2027-' || substr(replace(Id, '-', ''), 1, 22),
       COALESCE(RegisteredAt, CURRENT_TIMESTAMP),
       ParticipantName,
       COALESCE(Phone, ''),
       Email,
       '',
       trim(COALESCE(CarMake, '') || ' ' || COALESCE(CarModel, '')),
       LicensePlate,
       RegistrationType,
       RegistrationStatus,
       RegistrationFee,
       AmountPaid
FROM Registrations;
DROP TABLE Registrations;
ALTER TABLE Registrations_New RENAME TO Registrations;
CREATE UNIQUE INDEX IX_Registrations_RegistrationNumber ON Registrations(RegistrationNumber);
CREATE UNIQUE INDEX IX_Registrations_PlateNumber ON Registrations(PlateNumber);
CREATE INDEX IX_Registrations_Email ON Registrations(Email);
CREATE INDEX IX_Registrations_RegistrationType ON Registrations(RegistrationType);
CREATE INDEX IX_Registrations_Status ON Registrations(Status);
COMMIT;";
            await migration.ExecuteNonQueryAsync();
        }
    }
    await context.Database.CloseConnectionAsync();

    await context.Database.MigrateAsync();
    await RegistrationSeeder.SeedAsync(context);

    await context.Database.OpenConnectionAsync();
    await using (var sequenceCommand = connection.CreateCommand())
    {
        sequenceCommand.CommandText = @"
INSERT OR IGNORE INTO RegistrationNumberSequences (Id, LastNumber)
SELECT 1, COALESCE(MAX(CAST(substr(RegistrationNumber, 6) AS INTEGER)), 0)
FROM Registrations
WHERE RegistrationNumber GLOB 'CS27-[0-9]*';
UPDATE RegistrationNumberSequences
SET LastNumber = MAX(
    LastNumber,
    COALESCE((
        SELECT MAX(CAST(substr(RegistrationNumber, 6) AS INTEGER))
        FROM Registrations
        WHERE RegistrationNumber GLOB 'CS27-[0-9]*'
    ), 0)
)
WHERE Id = 1;";
        await sequenceCommand.ExecuteNonQueryAsync();
    }
    await context.Database.CloseConnectionAsync();
}

app.Run();
