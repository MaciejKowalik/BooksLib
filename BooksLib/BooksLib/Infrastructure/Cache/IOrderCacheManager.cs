using BooksLib.Domain.Models;

namespace BooksLib.Infrastructure.Cache
{
    /// <summary>
    /// Interface for cache manager class responsible for caching informations about orders
    /// </summary>
    public interface IOrderCacheManager
    {
        Task<List<OrderDTO>> GetCachedOrdersAsync();
        Task AddCachedOrdersAsync(List<OrderDTO> orders, TimeSpan expirationTime);
    }
}
