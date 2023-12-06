using System.Text.Json;

using PizzaManagement.Application.Common.Interfaces;

using StackExchange.Redis;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private bool _resetGetAllRecipesCache = false;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
            {
                return;
            }

            var serializedResponse = JsonSerializer.Serialize(response, _jsonSerializerOptions);

            await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            return cachedResponse.IsNullOrEmpty ? null : (string?)cachedResponse;
        }

        public async Task DeleteCacheKey(string cacheKey)
        {
            await _database.KeyDeleteAsync(cacheKey);
        }

        public void SetResetAllRecipeCache(bool status)
        {
            _resetGetAllRecipesCache = status;
        }

        public bool GetResetAllRecipeCache()
        {
            return _resetGetAllRecipesCache;
        }
    }
}