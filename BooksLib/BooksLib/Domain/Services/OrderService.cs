using BooksLib.Domain.Abstraction;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.GetOrders;
using BooksLib.Infrastructure;
using Newtonsoft.Json;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of OrderService class, responsible for operating on orders
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ExternalApiServiceWrapper _externalApiServiceWrapper;

        public OrderService(ExternalApiServiceWrapper externalApiServiceWrapper)
        {
            _externalApiServiceWrapper = externalApiServiceWrapper;
        }
        /// <summary>
        /// Method for getting the list of orders
        /// </summary>
        /// <param name="getOrdersRequest">Request parameters</param>
        /// <returns>List of orders</returns>
        public async Task<GetOrdersResponseDTO> GetOrdersAsync(GetOrdersRequestDTO getOrdersRequest)
        {
            var response = await _externalApiServiceWrapper.GetAsync(Constants.ApiOrdersCall);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ordersList = JsonConvert.DeserializeObject<List<OrderDTO>>(content);

                return new GetOrdersResponseDTO() { Orders = ordersList };
            }

            return new GetOrdersResponseDTO() { Orders = new List<OrderDTO>() };
        }
    }
}
