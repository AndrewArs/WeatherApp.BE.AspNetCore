namespace Application.Common.Models;

public readonly struct Error
{
    public ErrorCodes Code { get; }

    public string? Message { get; }

    public Error(ErrorCodes code = ErrorCodes.Undefined, string? message = default)
    {
        Code = code;
        Message = message;
    }

    public static Error NotFound<T>()
    {
        return new Error(ErrorCodes.NotFound, $"Entity {typeof(T).Name} not found");
    }

    public static Error ValidationFailed(string text)
    {
        return new Error(ErrorCodes.ValidationFailed, text);
    }

    public static Error Unhandled(string text)
    {
        return new Error(ErrorCodes.Unhandled, text);
    }
}
