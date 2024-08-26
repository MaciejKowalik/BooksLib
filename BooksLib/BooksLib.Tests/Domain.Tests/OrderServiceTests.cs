using AutoMapper;
using BooksLib.Domain.Services;
using BooksLib.DomainApi.Common;
using BooksLib.Infrastructure.Cache;
using BooksLib.Infrastructure;
using Moq;
using Microsoft.Extensions.Options;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.DTOs.GetOrders;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using FluentAssertions;
using BooksLib.DomainApi.Validators;

namespace BooksLib.Tests.Domain.Tests
{
    /// <summary>
    /// Class containing unit tests for methods from OrderService, using xUnit and additional libraries: Moq, FluentAssertion
    /// </summary>
    public class OrderServiceTests
    {
        private readonly Mock<IExternalApiServiceWrapper> _mockExternalApiServiceWrapper;
        private readonly Mock<IOrderCacheManager> _mockOrderCacheManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderService _orderService;
        private readonly BookLibOptions _options;

        public OrderServiceTests()
        {
            _mockExternalApiServiceWrapper = new Mock<IExternalApiServiceWrapper>();
            _mockOrderCacheManager = new Mock<IOrderCacheManager>();
            _mockMapper = new Mock<IMapper>();
            _options = new BookLibOptions
            {
                OrdersCacheExpirationTime = 10 // Example value for cache expiration
            };
            _orderService = new OrderService(
                _mockExternalApiServiceWrapper.Object,
                _mockOrderCacheManager.Object,
                new OptionsWrapper<BookLibOptions>(_options),
                _mockMapper.Object
            );
        }

        /// <summary>
        /// Test case testing a correct operation of getting orders - method GetOrdersAsync, should return NoErrors and correct list of orders
        /// </summary>
        [Fact]
        public async Task GetOrdersAsync_SuccessfulResponse_ReturnsOrdersList()
        {
            // Arrange
            var externalOrders = new List<ExternalOrderDTO>
            {
                new ExternalOrderDTO { OrderId = "5c54ba9c-175a-445f-88b2-003223caa187", OrderLines = new List<ExternalOrderLineDTO>() {new ExternalOrderLineDTO { BookId = 1, Quantity = 1} } },
                new ExternalOrderDTO { OrderId = "8e87609a-2693-4d74-9a6c-a7444b1101aa", OrderLines = new List<ExternalOrderLineDTO>() {new ExternalOrderLineDTO { BookId = 2, Quantity = 2} } },
            };

            var mappedOrders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = "5c54ba9c-175a-445f-88b2-003223caa187", OrderLines = new List<OrderLineDTO>() {new OrderLineDTO { BookId = 1, Quantity = 1} } },
                new OrderDTO { OrderId = "8e87609a-2693-4d74-9a6c-a7444b1101aa", OrderLines = new List<OrderLineDTO>() {new OrderLineDTO { BookId = 2, Quantity = 2} } },
            };

            var responseContent = JsonConvert.SerializeObject(externalOrders);
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };

            _mockExternalApiServiceWrapper.Setup(x => x.GetAsync(It.IsAny<string>()))
                                          .ReturnsAsync(Tuple.Create(ExitCodeEnum.NoErrors, httpResponseMessage));

            _mockMapper.Setup(x => x.Map<List<OrderDTO>>(It.IsAny<List<ExternalOrderDTO>>()))
                       .Returns(mappedOrders);

            // Act
            var result = await _orderService.GetOrdersAsync(new GetOrdersRequestDTO { PageNumber = 1, PageSize = 2 });

            // Assert
            result.Orders.Should().NotBeEmpty();
            result.ExitCode.Should().Be(ExitCodeEnum.NoErrors);
            result.Orders.Should().BeEquivalentTo(mappedOrders);
        }

        /// <summary>
        /// Test case testing GetOrdersAsync method, when given request is invalid, should return validation error
        /// </summary>
        [Fact]
        public async Task GetOrdersAsync_ValidationFails_ReturnsValidationError()
        {
            // Arrange
            var request = new GetOrdersRequestDTO(); // Invalid request without parameters
            var validator = new GetOrdersRequestDTOValidator();
            var validationResult = validator.Validate(request);

            _mockOrderCacheManager.Setup(x => x.GetCachedOrdersAsync())
                                  .ReturnsAsync((List<OrderDTO>)null);

            // Act
            var result = await _orderService.GetOrdersAsync(request);

            // Assert
            result.Orders.Should().BeEmpty();
            result.ExitCode.Should().Be(ExitCodeEnum.ValidationError);
            result.Message.Should().Contain(ResponseMessages.ValidationErrorMessage);
        }

        /// <summary>
        /// Test case testing a correct operation of gettings orders from cache - method GetOrdersAsync, should return NoErrors and correct list of orders
        /// </summary>
        [Fact]
        public async Task GetOrdersAsync_CacheHit_ReturnsCachedOrders()
        {
            // Arrange
            var cachedOrders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = "5c54ba9c-175a-445f-88b2-003223caa187", OrderLines = new List<OrderLineDTO>() {new OrderLineDTO { BookId = 1, Quantity = 1} } },
                new OrderDTO { OrderId = "8e87609a-2693-4d74-9a6c-a7444b1101aa", OrderLines = new List<OrderLineDTO>() {new OrderLineDTO { BookId = 2, Quantity = 2} } },
            };

            _mockOrderCacheManager.Setup(x => x.GetCachedOrdersAsync())
                                  .ReturnsAsync(cachedOrders);

            // Act
            var result = await _orderService.GetOrdersAsync(new GetOrdersRequestDTO { PageNumber = 1, PageSize = 2 });

            // Assert
            result.Orders.Should().NotBeEmpty();
            result.ExitCode.Should().Be(ExitCodeEnum.NoErrors);
            result.Orders.Should().BeEquivalentTo(cachedOrders);
        }
    }
}
