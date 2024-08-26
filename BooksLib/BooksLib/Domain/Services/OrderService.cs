using AutoMapper;
using BooksLib.Domain.Abstraction;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;
using BooksLib.DomainApi.Common;
using BooksLib.DomainApi.DTOs.GetBooks;
using BooksLib.DomainApi.DTOs.GetOrders;
using BooksLib.DomainApi.Validators;
using BooksLib.Infrastructure;
using BooksLib.Infrastructure.Cache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BooksLib.Domain.Services
{
    /// <summary>
    /// File containing implementation of OrderService class, responsible for operating on orders
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ExternalApiServiceWrapper _externalApiServiceWrapper;
        private readonly IOrderCacheManager _orderCacheManager;
        private readonly IMapper _mapper;
        private readonly BookLibOptions _options;

        public OrderService(ExternalApiServiceWrapper externalApiServiceWrapper, IOrderCacheManager orderCacheManager,
            IOptions<BookLibOptions> options, IMapper mapper)
        {
            _externalApiServiceWrapper = externalApiServiceWrapper;
            _orderCacheManager = orderCacheManager;
            _mapper = mapper;
            _options = options.Value;
        }

        /// <summary>
        /// Method for getting the list of orders
        /// </summary>
        /// <param name="getOrdersRequest">Request parameters</param>
        /// <returns>List of orders</returns>
        public async Task<GetOrdersResponseDTO> GetOrdersAsync(GetOrdersRequestDTO getOrdersRequest)
        {
            var requestValidator = new GetOrdersRequestDTOValidator();
            var validationResult = requestValidator.Validate(getOrdersRequest);

            if (!validationResult.IsValid)
            {
                return new GetOrdersResponseDTO
                {
                    Orders = new List<OrderDTO>(),
                    ExitCode = ExitCodeEnum.ValidationError,
                    Message = ResponseMessages.ValidationErrorMessage + validationResult.Errors.Select(x => x.ErrorMessage)
                };
            }

            var cachedOrders = await _orderCacheManager.GetCachedOrdersAsync();

            if (cachedOrders != null)
            {
                return GetPaginatedResult(cachedOrders, getOrdersRequest.PageNumber, getOrdersRequest.PageSize);
            }

            var response = await _externalApiServiceWrapper.GetAsync(Constants.ApiOrdersCall);

            if (response.Item1 == ExitCodeEnum.NoErrors)
            {
                var content = await response.Item2.Content.ReadAsStringAsync();
                var ordersList = new List<ExternalOrderDTO>();
                var mappedOrdersList = new List<OrderDTO>();

                try
                {
                    ordersList = JsonConvert.DeserializeObject<List<ExternalOrderDTO>>(content);
                }
                catch (Exception ex)
                {
                    return new GetOrdersResponseDTO
                    {
                        Orders = new List<OrderDTO>(),
                        ExitCode = ExitCodeEnum.SerializeDeserializeError,
                        Message = ResponseMessages.SerializationDeserializationError + ex.Message,
                    };
                }

                try
                {
                    mappedOrdersList = _mapper.Map<List<OrderDTO>>(ordersList);
                }
                catch (Exception ex)
                {
                    return new GetOrdersResponseDTO
                    {
                        Orders = new List<OrderDTO>(),
                        ExitCode = ExitCodeEnum.MappingError,
                        Message = ResponseMessages.MappingErrorMessage + ex.Message,
                    };
                }

                await _orderCacheManager.AddCachedOrdersAsync(mappedOrdersList, TimeSpan.FromMinutes(_options.OrdersCacheExpirationTime));

                return GetPaginatedResult(mappedOrdersList, getOrdersRequest.PageNumber, getOrdersRequest.PageSize);
            }

            return new GetOrdersResponseDTO { Orders = new List<OrderDTO>(), Metainfo = new MetainfoDTO { Count = 0, TotalCount = 0 }, ExitCode = response.Item1, Message = MessagesProvider.GetMessageForExitCode(response.Item1)};
        }

        private GetOrdersResponseDTO GetPaginatedResult(List<OrderDTO> orders, int pageNumber, int pageSize)
        {
            var paginatedOrders = orders.Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            return new GetOrdersResponseDTO
            {
                Orders = paginatedOrders,
                Metainfo = new MetainfoDTO { Count = paginatedOrders.Count, TotalCount = orders.Count } };
        }
    }
}
