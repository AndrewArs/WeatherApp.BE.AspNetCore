namespace Application.Common.Models;

public static class Result
{
    public static Result<Empty> Empty() => new(new Empty());

    public static Result<T?> Success<T>(T value) => new(value);

    public static Result<T> Failure<T>(Error error) => new(error);

    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Error, TOut> failure) 
        => result.IsSuccess ? success(result.Value) : failure(result.Error);
}
