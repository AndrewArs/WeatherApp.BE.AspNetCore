namespace Application.Common.Models;

public readonly struct Result<T> : IResult
{
    public T Value { get; } = default!;

    public Error Error { get; }

    public bool IsSuccess { get; }

    public Result(T value)
    {
        Value = value;
        Error = default;
        IsSuccess = true;
    }

    public Result(Error error)
    {
        Value = default!;
        Error = error;
        IsSuccess = false;
    }
    
    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);
}
