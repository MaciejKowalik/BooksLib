namespace BooksLib.Domain.Models
{
    public class OrderLineDTO
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
