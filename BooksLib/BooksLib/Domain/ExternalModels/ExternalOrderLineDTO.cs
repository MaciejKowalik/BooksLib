namespace BooksLib.Domain.ExternalModels
{
    public class ExternalOrderLineDTO
    {
        /// <summary>
        /// Book's id
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Quantity of books ordered
        /// </summary>
        public int Quantity { get; set; }
    }
}