using BooksLib.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BooksLib.Infrastructure.Cache
{
    /// <summary>
    /// Cache class managing the storage of order information in form of a InMemoryCache
    /// </summary>
    public class OrderCacheManager : IOrderCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private const string _ordersCacheKey = "ordersCacheKey";
        public OrderCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Method adding orders to cache
        /// </summary>
        /// <param name="orders">List of orders to add to cache</param>
        /// <param name="expirationTime">Expiration time for cache in minutes</param>
        /// <returns></returns>
        public Task AddCachedOrdersAsync(List<OrderDTO> orders, TimeSpan expirationTime)
        {
            _memoryCache.Set(_ordersCacheKey, orders, expirationTime);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method getting informations about orders from cache
        /// </summary>
        /// <returns>List of orders stored in cache</returns>
        public Task<List<OrderDTO>> GetCachedOrdersAsync()
        {
            _memoryCache.TryGetValue(_ordersCacheKey, out List<OrderDTO> orders);
            return Task.FromResult(orders);
        }
    }
}
