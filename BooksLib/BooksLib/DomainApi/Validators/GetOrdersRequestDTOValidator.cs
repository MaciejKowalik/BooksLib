using BooksLib.DomainApi.DTOs.GetOrders;
using FluentValidation;

namespace BooksLib.DomainApi.Validators
{
    /// <summary>
    /// Validation class for validating GetOrdersRequestDTO values, using FluentValidation library
    /// </summary>
    public class GetOrdersRequestDTOValidator : AbstractValidator<GetOrdersRequestDTO>
    {
        public GetOrdersRequestDTOValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty().GreaterThan(0);
            RuleFor(x => x.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
