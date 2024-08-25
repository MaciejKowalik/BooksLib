using BooksLib.Domain.Abstraction;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.GetOrders;
using BooksLib.Infrastructure;
using BooksLib.Infrastructure.Cache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of OrderService class, responsible for operating on orders
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ExternalApiServiceWrapper _externalApiServiceWrapper;
        private readonly IOrderCacheManager _orderCacheManager;
        private readonly BookLibOptions _options;

        public OrderService(ExternalApiServiceWrapper externalApiServiceWrapper, IOrderCacheManager orderCacheManager,
            IOptions<BookLibOptions> options)
        {
            _externalApiServiceWrapper = externalApiServiceWrapper;
            _orderCacheManager = orderCacheManager;
            _options = options.Value;
        }

        /// <summary>
        /// Method for getting the list of orders
        /// </summary>
        /// <param name="getOrdersRequest">Request parameters</param>
        /// <returns>List of orders</returns>
        public async Task<GetOrdersResponseDTO> GetOrdersAsync(GetOrdersRequestDTO getOrdersRequest)
        {
            var cachedOrders = await _orderCacheManager.GetCachedOrdersAsync();

            if (cachedOrders != null)
            {
                return GetPaginatedResult(cachedOrders, getOrdersRequest.PageNumber, getOrdersRequest.PageSize);
            }

            var response = await _externalApiServiceWrapper.GetAsync(Constants.ApiOrdersCall);

            if (response.Item1 == ExitCodeEnum.NoErrors)
            {
                var content = await response.Item2.Content.ReadAsStringAsync();
                var ordersList = JsonConvert.DeserializeObject<List<OrderDTO>>(content);

                await _orderCacheManager.AddCachedOrdersAsync(ordersList, TimeSpan.FromMinutes(_options.OrdersCacheExpirationTime));

                return GetPaginatedResult(ordersList, getOrdersRequest.PageNumber, getOrdersRequest.PageSize);
            }

            return new GetOrdersResponseDTO() { Orders = new List<OrderDTO>(), ExitCode = response.Item1};
        }

        private GetOrdersResponseDTO GetPaginatedResult(List<OrderDTO> orders, int pageNumber, int pageSize)
        {
            var paginatedOrders = orders.Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            return new GetOrdersResponseDTO() { Orders = paginatedOrders };
        }
    }
}
