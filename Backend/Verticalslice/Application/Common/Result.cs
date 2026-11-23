namespace CarShowRegistration.Application.Common;

public enum ResultErrorType
{
    Validation,
    NotFound,
    Conflict
}

public sealed record ResultError(
    ResultErrorType Type,
    string Code,
    string Description,
    IDictionary<string, string[]>? ValidationErrors = null)
{
    public static ResultError Validation(IDictionary<string, string[]> errors) =>
        new(ResultErrorType.Validation, "Validation.Error", "One or more validation errors occurred.", errors);

    public static ResultError NotFound(string code, string description) =>
        new(ResultErrorType.NotFound, code, description);

    public static ResultError Conflict(string code, string description) =>
        new(ResultErrorType.Conflict, code, description);
}

public sealed class Result<T>
{
    private Result(bool isSuccess, T? value, ResultError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public ResultError? Error { get; }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(ResultError error) => new(false, default, error);
}
