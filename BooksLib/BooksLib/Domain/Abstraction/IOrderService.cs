using BooksLib.DomainApi.DTOs.GetOrders;

namespace BooksLib.Domain.Abstraction
{
    /// <summary>
    /// Service interface for orders operations
    /// </summary>
    public interface IOrderService
    {
        Task<GetOrdersResponseDTO> GetOrdersAsync(GetOrdersRequestDTO getOrdersRequest);
    }
}
