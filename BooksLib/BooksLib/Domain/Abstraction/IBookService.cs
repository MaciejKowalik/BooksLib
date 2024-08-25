using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.AddBook;
using BooksLib.DomainApi.DTOs.GetBooks;

namespace BooksLib.Domain.Abstraction
{
    /// <summary>
    /// Service interface for books operations
    /// </summary>
    public interface IBookService
    {
        Task<GetBooksResponseDTO> GetBooksAsync();
        Task<BaseResponseDTO> AddBookAsync(AddBookRequestDTO addBookRequest);
    }
}
