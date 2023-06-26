using MediatR;

namespace Application.Common.Interfaces.Mediatr;

public interface IRequestResult<TResult> : IRequest<Result<TResult>>
{
}

public interface IRequestResult : IRequest<Result<EmptyResult>>
{
}