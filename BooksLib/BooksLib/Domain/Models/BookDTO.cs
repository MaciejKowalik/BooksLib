namespace BooksLib.Domain.Models
{
    public class BookDTO
    {
        /// <summary>
        /// Book id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Book title
        /// </summary>
        public int Title { get; set; }

        /// <summary>
        /// Book price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Bookstand number
        /// </summary>
        public int Bookstand { get; set; }

        /// <summary>
        /// Shelf number
        /// </summary>
        public int Shelf { get; set; }

        /// <summary>
        /// List of book's authors
        /// </summary>
        public ICollection<AuthorDTO> Authors { get; set; }
    }
}
