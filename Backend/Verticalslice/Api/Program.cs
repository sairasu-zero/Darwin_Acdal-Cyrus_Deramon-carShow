using System.Text.Json.Serialization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using CarShowRegistration.Api.Endpoints.Registrations;
using CarShowRegistration.Application;
using CarShowRegistration.Infrastructure;
using CarShowRegistration.Infrastructure.Persistence;
using CarShowRegistration.Infrastructure.Persistence.Seed;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var backendDirectory = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", ".."));
var databasePath = Path.Combine(backendDirectory, "carshow.db");
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
    await context.Database.EnsureCreatedAsync();

    await context.Database.OpenConnectionAsync();
    var connection = (SqliteConnection)context.Database.GetDbConnection();
    await using (var columnCommand = connection.CreateCommand())
    {
        columnCommand.CommandText = "SELECT COUNT(*) FROM pragma_table_info('Registrations') WHERE name = 'RegistrationNumber';";
        var hasNewSchema = Convert.ToInt64(await columnCommand.ExecuteScalarAsync()) > 0;

        if (!hasNewSchema)
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
    CarMakeModel TEXT NOT NOT NULL,
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
            migration.CommandText = migration.CommandText.Replace("CarMakeModel TEXT NOT NOT NULL", "CarMakeModel TEXT NOT NULL");
            await migration.ExecuteNonQueryAsync();
        }
    }
    await context.Database.CloseConnectionAsync();

    await RegistrationSeeder.SeedAsync(context);
}

app.Run();
