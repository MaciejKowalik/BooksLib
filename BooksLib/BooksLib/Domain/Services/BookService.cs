using AutoMapper;
using BooksLib.Domain.Abstraction;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.AddBook;
using BooksLib.DomainApi.DTOs.GetBooks;
using BooksLib.DomainApi.Validators;
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
        private readonly IExternalApiServiceWrapper _externalApiServiceWrapper;
        private readonly IMapper _mapper;

        public BookService(IExternalApiServiceWrapper externalApiServiceWrapper, IMapper mapper)
        {
            _externalApiServiceWrapper = externalApiServiceWrapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for adding a book via external API
        /// </summary>
        /// <param name="addBookRequest">Request parameters</param>
        /// <returns>Base response</returns>
        public async Task<BaseResponseDTO> AddBookAsync(AddBookRequestDTO addBookRequest)
        {
            var requestValidator = new AddBookRequestDTOValidator();
            var validationResult = requestValidator.Validate(addBookRequest);

            var mappedBook = new ExternalBookDTO();
            var serializedBook = string.Empty;

            if (!validationResult.IsValid)
            {
                return new BaseResponseDTO
                {
                    ExitCode = ExitCodeEnum.ValidationError,
                    Message = ResponseMessages.ValidationErrorMessage + validationResult.Errors.Select(x => x.ErrorMessage)
                };
            }
            try
            {
                mappedBook = _mapper.Map<ExternalBookDTO>(addBookRequest.Book);
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO
                {
                    ExitCode = ExitCodeEnum.MappingError,
                    Message = ResponseMessages.MappingErrorMessage + ex.Message,
                };
            }

            try
            {
                serializedBook = JsonConvert.SerializeObject(mappedBook);
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO
                {
                    ExitCode = ExitCodeEnum.SerializeDeserializeError,
                    Message = ResponseMessages.SerializationDeserializationError + ex.Message,
                };
            }

            var content = new StringContent(serializedBook, Encoding.UTF8, Constants.ApplicationJsonFormat);
            var response = await _externalApiServiceWrapper.PostAsync(Constants.ApiBooksCall, content);

            return new BaseResponseDTO() { ExitCode = response.Item1, Message = MessagesProvider.GetMessageForExitCode(response.Item1) };
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
                var bookList = new List<ExternalBookDTO>();
                var mappedBookList = new List<BookDTO>();

                try
                {
                    bookList = JsonConvert.DeserializeObject<List<ExternalBookDTO>>(content);
                }
                catch (Exception ex)
                {
                    return new GetBooksResponseDTO
                    {
                        Books = new List<BookDTO>(),
                        ExitCode = ExitCodeEnum.SerializeDeserializeError,
                        Message = ResponseMessages.SerializationDeserializationError + ex.Message,
                    };
                }

                try
                {
                    mappedBookList = _mapper.Map<List<BookDTO>>(bookList);
                }
                catch (Exception ex)
                {
                    return new GetBooksResponseDTO
                    {
                        Books = new List<BookDTO>(),
                        ExitCode = ExitCodeEnum.MappingError,
                        Message = ResponseMessages.MappingErrorMessage + ex.Message,
                    };
                }

                return new GetBooksResponseDTO { Books = mappedBookList };
            }

            return new GetBooksResponseDTO { Books = new List<BookDTO>(), ExitCode = response.Item1, Message = MessagesProvider.GetMessageForExitCode(response.Item1)};
        }
    }
}
