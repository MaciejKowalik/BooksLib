using BooksLib.Domain.Abstraction;
using BooksLib.DomainApi.DTOs.GetOrders;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of OrderService class, responsible for operating on orders
    /// </summary>
    public class OrderService : IOrderService
    {
        /// <summary>
        /// Method for getting the list of orders
        /// </summary>
        /// <param name="getOrdersRequest">Request parameters</param>
        /// <returns>List of orders</returns>
        public Task<GetOrdersResponseDTO> GetOrdersAsync(GetOrdersRequestDTO getOrdersRequest)
        {
            throw new NotImplementedException();
        }
    }
}
