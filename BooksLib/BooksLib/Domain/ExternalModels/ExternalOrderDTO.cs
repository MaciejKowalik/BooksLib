namespace BooksLib.Domain.ExternalModels
{
    public class ExternalOrderDTO
    {
        /// <summary>
        /// Order Id in GUID format
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// List with contents of the order
        /// </summary>
        public ICollection<ExternalOrderLineDTO> OrderLines { get; set; }
    }
}
