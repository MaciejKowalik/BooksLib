using AutoMapper;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;
using BooksLib.Domain.Services;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.AddBook;
using BooksLib.DomainApi.Validators;
using BooksLib.Infrastructure;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BooksLib.Tests.Domain.Tests
{
    /// <summary>
    /// Class containing unit tests for methods from BookService, using xUnit and additional libraries: Moq, FluentAssertion
    /// </summary>
    public class BookServiceTests
    {
        private readonly Mock<IExternalApiServiceWrapper> _mockExternalApiServiceWrapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockExternalApiServiceWrapper = new Mock<IExternalApiServiceWrapper>();
            _mockMapper = new Mock<IMapper>();
            _bookService = new BookService(_mockExternalApiServiceWrapper.Object, _mockMapper.Object);
        }

        /// <summary>
        /// Test case testing AddBookAsync method, when given request is invalid, should return validation error
        /// </summary>
        [Fact]
        public async Task AddBookAsync_InvalidRequest_ReturnsValidationError()
        {
            // Arrange
            var request = new AddBookRequestDTO { Book = new BookDTO { Shelf = -1 }
            };
            var validator = new AddBookRequestDTOValidator();
            var validationResult = validator.Validate(request);
            _mockMapper.Setup(x => x.Map<ExternalBookDTO>(It.IsAny<BookDTO>()))
                       .Throws(new Exception("Mapping failed"));

            // Act
            var result = await _bookService.AddBookAsync(request);

            // Assert
            result.ExitCode.Should().Be(ExitCodeEnum.ValidationError);
            result.Message.Should().Contain(ResponseMessages.ValidationErrorMessage);
        }

        /// <summary>
        /// Test case testing a correct operation of adding book - method AddBookAsync, should return NoErrors
        /// </summary>
        [Fact]
        public async Task AddBookAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange

            var requestBook = new BookDTO { Id = 1, Title = "Book One", Price = 10, Bookstand = 1, Shelf = 1, Authors = new List<AuthorDTO> { new AuthorDTO { FirstName = "John", LastName = "Smith" } } };

            var request = new AddBookRequestDTO
            {
                Book = requestBook
            };
            var expectedResponse = new BaseResponseDTO { ExitCode = ExitCodeEnum.NoErrors, Message = ResponseMessages.NoErrorsMessage};
            _mockExternalApiServiceWrapper.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                                  .ReturnsAsync(Tuple.Create(ExitCodeEnum.NoErrors, Mock.Of<HttpResponseMessage>()));

            // Act
            var result = await _bookService.AddBookAsync(request);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        /// <summary>
        /// Test case testing a correct operation of getting books - method GetBooksASync, should return NoErrors and correct list of books
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetBooksAsync_SuccessfulResponse_ReturnsBooksList()
        {
            // Arrange
            var externalBooks = new List<ExternalBookDTO>
            {
                new ExternalBookDTO { Id = 1, Title = "Book One", Price = 10, Bookstand = 1, Shelf = 1, Authors = new List<ExternalAuthorDTO> { new ExternalAuthorDTO { FirstName = "John", LastName = "Smith" } } },
                new ExternalBookDTO { Id = 2, Title = "Book One", Price = 20, Bookstand = 2, Shelf = 2, Authors = new List<ExternalAuthorDTO> { new ExternalAuthorDTO { FirstName = "Anna", LastName = "Stakes" } } }
            };

            var mappedBooks = new List<BookDTO>
            {
                new BookDTO { Id = 1, Title = "Book One", Price = 10, Bookstand = 1, Shelf = 1, Authors = new List<AuthorDTO> { new AuthorDTO { FirstName = "John", LastName = "Smith" } } },
                new BookDTO { Id = 2, Title = "Book One", Price = 20, Bookstand = 2, Shelf = 2, Authors = new List<AuthorDTO> { new AuthorDTO { FirstName = "Anna", LastName = "Stakes" } } }
            };

            var responseContent = JsonConvert.SerializeObject(externalBooks);
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };

            _mockExternalApiServiceWrapper.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(Tuple.Create(ExitCodeEnum.NoErrors, httpResponseMessage));

            _mockMapper.Setup(x => x.Map<List<BookDTO>>(It.IsAny<List<ExternalBookDTO>>()))
                       .Returns(mappedBooks);

            // Act
            var result = await _bookService.GetBooksAsync();

            // Assert
            result.Books.Should().NotBeEmpty();
            result.ExitCode.Should().Be(ExitCodeEnum.NoErrors);
        }
    }
}
