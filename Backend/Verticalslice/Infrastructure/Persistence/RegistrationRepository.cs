using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarShowRegistration.Infrastructure.Persistence;

public sealed class RegistrationRepository(AppDbContext context) : IRegistrationRepository
{
    public Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Registrations.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<(IReadOnlyList<Registration> Items, int TotalCount)> GetAsync(
        string? participantName,
        RegistrationType? type,
        RegistrationStatus? status,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = context.Registrations.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(participantName))
        {
            var escapedName = participantName
                .Replace("\\", "\\\\", StringComparison.Ordinal)
                .Replace("%", "\\%", StringComparison.Ordinal)
                .Replace("_", "\\_", StringComparison.Ordinal);
            query = query.Where(x => EF.Functions.Like(
                EF.Functions.Collate(x.ParticipantName, "NOCASE"),
                $"%{escapedName}%",
                "\\"));
        }
        if (type.HasValue)
            query = query.Where(x => x.RegistrationType == type);
        if (status.HasValue)
            query = query.Where(x => x.Status == status);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(x => x.RegistrationDate)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<RegistrationSummaryData> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var aggregate = await context.Registrations
            .AsNoTracking()
            .GroupBy(_ => 1)
            .Select(group => new RegistrationSummaryData(
                group.Count(),
                group.Count(x => x.Status == RegistrationStatus.Confirmed),
                group.Count(x => x.Status == RegistrationStatus.CheckedIn),
                group.Count(x => x.Status == RegistrationStatus.Pending),
                group.Count(x => x.Status == RegistrationStatus.Cancelled),
                group.Where(x => x.Status != RegistrationStatus.Cancelled).Sum(x => (decimal?)x.RegistrationFee) ?? 0m,
                group.Where(x => x.Status != RegistrationStatus.Cancelled).Sum(x => (decimal?)x.AmountPaid) ?? 0m))
            .SingleOrDefaultAsync(cancellationToken);

        return aggregate ?? new RegistrationSummaryData(0, 0, 0, 0, 0, 0m, 0m);
    }

    public Task<bool> PlateNumberExistsAsync(string plateNumber, CancellationToken cancellationToken = default) =>
        context.Registrations.AnyAsync(
            x => EF.Functions.Collate(x.PlateNumber, "NOCASE") == plateNumber,
            cancellationToken);

    public async Task<bool> AddAsync(Registration registration, CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await using var command = context.Database.GetDbConnection().CreateCommand();
            command.Transaction = context.Database.CurrentTransaction!.GetDbTransaction();
            command.CommandText = "UPDATE RegistrationNumberSequences SET LastNumber = LastNumber + 1 WHERE Id = 1 RETURNING LastNumber;";

            var result = await command.ExecuteScalarAsync(cancellationToken);
            if (result is null or DBNull)
                throw new InvalidOperationException("The registration number sequence is not initialized.");

            registration.RegistrationNumber = $"CS27-{Convert.ToInt64(result):D4}";
            context.Registrations.Add(registration);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException exception) when (IsPlateNumberUniqueViolation(exception))
        {
            return false;
        }
    }

    private static bool IsPlateNumberUniqueViolation(DbUpdateException exception) =>
        exception.InnerException is SqliteException sqliteException
        && sqliteException.SqliteExtendedErrorCode == 2067
        && sqliteException.Message.Contains("Registrations.PlateNumber", StringComparison.OrdinalIgnoreCase);
}
