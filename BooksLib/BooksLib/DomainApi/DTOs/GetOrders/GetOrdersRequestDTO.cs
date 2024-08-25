namespace BooksLib.DomainApi.DTOs.GetOrders
{
    /// <summary>
    /// Request class for method, getting list of orders
    /// </summary>
    public class GetOrdersRequestDTO
    {
        /// <summary>
        /// Quantity of returned results
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Number of page of returned results
        /// </summary>
        public int PageNumber { get; set; }
    }
}
