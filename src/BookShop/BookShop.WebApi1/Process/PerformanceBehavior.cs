using MediatR;
using System.Diagnostics;

namespace BookShop.WebApi1.Process
{
    public class PerformanceBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest 
        : IRequest<TResponse>
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PerformanceBehavior(ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _stopwatch = new();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _stopwatch.Start();
            var response = await next();
            _stopwatch.Stop();

            var elapsed = _stopwatch.ElapsedMilliseconds;
            var requestName = typeof(TRequest).Name;
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name ?? string.Empty;

            _logger.LogWarning($"Log running request with EntityFrameworkCore : {requestName} {elapsed} ms. {userName}");

            return response;
        }
    }
}
