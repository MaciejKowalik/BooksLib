using BooksLib.Domain.Abstraction;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.AddBook;
using BooksLib.DomainApi.DTOs.GetBooks;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of BookService class, responsible for operating on books
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// Method for adding a book via external API
        /// </summary>
        /// <param name="addBookRequest">Request parameters</param>
        /// <returns>Base response</returns>
        public Task<BaseResponseDTO> AddBookAsync(AddBookRequestDTO addBookRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for getting the list of books
        /// </summary>
        /// <returns>List of books</returns>
        public Task<GetBooksResponseDTO> GetBooksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
