using CarShowRegistration.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarShowRegistration.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result, Func<T, IResult> onSuccess)
    {
        if (result.IsSuccess)
            return onSuccess(result.Value!);

        var error = result.Error!;
        return error.Type switch
        {
            ResultErrorType.Validation => Results.ValidationProblem(
                error.ValidationErrors!,
                statusCode: StatusCodes.Status400BadRequest,
                title: error.Description),
            ResultErrorType.NotFound => Results.Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: error.Code,
                detail: error.Description),
            ResultErrorType.Conflict => Results.Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: error.Code,
                detail: error.Description),
            _ => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Unexpected result error",
                detail: error.Description)
        };
    }
}
