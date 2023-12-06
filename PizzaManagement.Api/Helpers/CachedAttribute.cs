using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PizzaManagement.Application.Common.Interfaces;

namespace PizzaManagement.Api.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            if(cachedService.GetResetAllRecipeCache())
            {
                await cachedService.DeleteCacheKey(cacheKey);
                cachedService.SetResetAllRecipeCache(false);
            }

            var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);

            // If there is something in cache, then we send the response ourselfs.
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }

            // Move to the controller, give back the control
            var executedContext = await next();

            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value!, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }

        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.AppendLine($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}