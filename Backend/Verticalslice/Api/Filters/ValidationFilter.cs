using CarShowRegistration.Api.Extensions;
using CarShowRegistration.Application.Common;
using FluentValidation;

namespace CarShowRegistration.Api.Filters;
public sealed class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.OfType<T>().FirstOrDefault();
        if (argument is null) return await next(context);
        var validation = await validator.ValidateAsync(argument, context.HttpContext.RequestAborted);
        if (validation.IsValid)
            return await next(context);

        var result = Result<object>.Failure(ResultError.Validation(validation.ToDictionary()));
        return result.ToHttpResult(_ => Results.Ok());
    }
}
