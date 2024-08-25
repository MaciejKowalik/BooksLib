using BooksLib.Domain.Abstraction;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.AddBook;
using BooksLib.DomainApi.DTOs.GetBooks;
using BooksLib.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of BookService class, responsible for operating on books
    /// </summary>
    public class BookService : IBookService
    {
        private readonly ExternalApiServiceWrapper _externalApiServiceWrapper;

        public BookService(ExternalApiServiceWrapper externalApiServiceWrapper)
        {
            _externalApiServiceWrapper = externalApiServiceWrapper;
        }

        /// <summary>
        /// Method for adding a book via external API
        /// </summary>
        /// <param name="addBookRequest">Request parameters</param>
        /// <returns>Base response</returns>
        public async Task<BaseResponseDTO> AddBookAsync(AddBookRequestDTO addBookRequest)
        {
            var serializedBook = JsonConvert.SerializeObject(addBookRequest.Book);
            var content = new StringContent(serializedBook, Encoding.UTF8, Constants.ApplicationJsonFormat);

            var response = await _externalApiServiceWrapper.PostAsync(Constants.ApiBooksCall, content);

            return new BaseResponseDTO() { ExitCode = response.Item1 };
        }

        /// <summary>
        /// Method for getting the list of books
        /// </summary>
        /// <returns>List of books</returns>
        public async Task<GetBooksResponseDTO> GetBooksAsync()
        {
            var response = await _externalApiServiceWrapper.GetAsync(Constants.ApiBooksCall);

            if (response.Item1 == ExitCodeEnum.NoErrors)
            {
                var content = await response.Item2.Content.ReadAsStringAsync();
                var bookList = JsonConvert.DeserializeObject<List<BookDTO>>(content);

                return new GetBooksResponseDTO() { Books = bookList };
            }

            return new GetBooksResponseDTO() { Books = new List<BookDTO>(), ExitCode = response.Item1 };
        }
    }
}
