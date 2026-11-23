using CarShowRegistration.Application.Features.Registrations.CreateRegistration;
using GetRegistrationsQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrations.Query;
using GetRegistrationByIdQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrationById.Query;
using GetRegistrationSummaryQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary.Query;
using CarShowRegistration.Application.Features.Registrations.GetRegistrationById;
using CarShowRegistration.Application.Features.Registrations.GetRegistrations;
using CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;
using CarShowRegistration.Domain.Entities;
using CarShowRegistration.Api.Filters;
namespace CarShowRegistration.Api.Endpoints.Registrations;
public static class RegistrationEndpoints
{
    public static void MapRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations").WithTags("Registrations");
        group.MapPost("", async (Request request, CreateRegistrationHandler handler, CancellationToken ct) =>
        {
            var response = await handler.HandleAsync(new Command(
                request.ParticipantName,
                request.ContactNumber,
                request.Email,
                request.Address,
                request.CarMakeModel,
                request.PlateNumber,
                request.RegistrationType,
                request.AmountPaid), ct);

            return Results.Created($"/api/registrations/{response.Id}", response);
        }).AddEndpointFilter<ValidationFilter<Request>>();
        group.MapGet("", async (RegistrationType? registrationType, RegistrationStatus? registrationStatus, GetRegistrationsHandler handler, CancellationToken ct) => Results.Ok(await handler.HandleAsync(new GetRegistrationsQuery(registrationType, registrationStatus), ct)));
        group.MapGet("/{id:guid}", async (Guid id, GetRegistrationByIdHandler handler, CancellationToken ct) => { var response = await handler.HandleAsync(new GetRegistrationByIdQuery(id), ct); return response is null ? Results.NotFound() : Results.Ok(response); });
        group.MapGet("/summary", async (GetRegistrationSummaryHandler handler, CancellationToken ct) => Results.Ok(await handler.HandleAsync(new GetRegistrationSummaryQuery(), ct)));
    }
}
