using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;

namespace BooksLib.DomainApi.DTOs.GetOrders
{
    /// <summary>
    /// Response class for method, returning collection of orders
    /// </summary>
    public class GetOrdersResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// List of orders
        /// </summary>
        public ICollection<OrderDTO> Orders { get; set; }

        /// <summary>
        /// Metainfo
        /// </summary>
        public MetainfoDTO Metainfo { get; set; }
    }
}
