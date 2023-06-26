using MediatR;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        var result = await next.Invoke();

        var elapsed = sw.ElapsedMilliseconds;

        if(elapsed > 500) // todo: some SLA settings
        {
            _logger.LogWarning("Performance issue. {ElapsedMilliseconds}, {Request}", elapsed, request);
        }

        return result;
    }
}
