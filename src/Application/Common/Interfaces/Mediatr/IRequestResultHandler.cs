using MediatR;

namespace Application.Common.Interfaces.Mediatr;

public interface IRequestResultHandler<in TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>>
    where TRequest : IRequestResult<TResult>
{
}

public interface IRequestResultHandler<in TRequest> : IRequestHandler<TRequest, Result<Empty>>
    where TRequest : IRequestResult
{
}