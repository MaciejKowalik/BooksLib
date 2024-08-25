using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;

namespace BooksLib.DomainApi.DTOs.GetBooks
{
    /// <summary>
    /// Response class for method, returning collection of books
    /// </summary>
    public class GetBooksResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// List of books
        /// </summary>
        public ICollection<BookDTO> Books { get; set; }
    }
}
