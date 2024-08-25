using BooksLib.Domain.Models;

namespace BooksLib.DomainApi.DTOs.AddBook
{
    /// <summary>
    /// Request class of method, adding new book via external API
    /// </summary>
    public class AddBookRequestDTO
    {
        /// <summary>
        /// Added book object
        /// </summary>
        public BookDTO Book { get; set; }
    }
}
