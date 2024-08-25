namespace BooksLib.Domain.Models
{
    public class OrderDTO
    {
        /// <summary>
        /// Order Id in GUID format
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// List with contents of the order
        /// </summary>
        public ICollection<OrderLineDTO> OrderLines { get; set; }
    }
}
