using CarShowRegistration.Api.Extensions;
using CarShowRegistration.Api.Filters;
using CarShowRegistration.Application.Features.Registrations.CreateRegistration;
using CarShowRegistration.Application.Features.Registrations.GetRegistrationById;
using CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;
using CarShowRegistration.Application.Features.Registrations.GetRegistrations;
using GetRegistrationsQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrations.Query;
using GetRegistrationByIdQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrationById.Query;
using GetRegistrationSummaryQuery = CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary.Query;

namespace CarShowRegistration.Api.Endpoints.Registrations;
public static class RegistrationEndpoints
{
    public static void MapRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations").WithTags("Registrations");
        group.MapPost("", async (Request request, CreateRegistrationHandler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(new Command(
                request.ParticipantName,
                request.ContactNumber,
                request.Email,
                request.Address,
                request.CarMakeModel,
                request.PlateNumber,
                request.RegistrationType,
                request.AmountPaid), ct);

            return result.ToHttpResult(response =>
                Results.Created($"/api/registrations/{response.Id}", response));
        }).AddEndpointFilter<ValidationFilter<Request>>();
        group.MapGet("", async ([AsParameters] GetRegistrationsQuery query, GetRegistrationsHandler handler, CancellationToken ct) =>
            (await handler.HandleAsync(query, ct)).ToHttpResult(Results.Ok))
            .AddEndpointFilter<ValidationFilter<GetRegistrationsQuery>>();
        group.MapGet("/summary", async (GetRegistrationSummaryHandler handler, CancellationToken ct) =>
            (await handler.HandleAsync(new GetRegistrationSummaryQuery(), ct)).ToHttpResult(Results.Ok));
        group.MapGet("/{id:guid}", async (Guid id, GetRegistrationByIdHandler handler, CancellationToken ct) =>
            (await handler.HandleAsync(new GetRegistrationByIdQuery(id), ct)).ToHttpResult(Results.Ok));
    }
}
