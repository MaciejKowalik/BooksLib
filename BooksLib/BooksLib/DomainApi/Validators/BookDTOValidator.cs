using BooksLib.Domain.Models;
using FluentValidation;

namespace BooksLib.DomainApi.Validators
{
    /// <summary>
    /// Validation class for validating BookDTO values, using FluentValidation library
    /// </summary>
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Bookstand).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Shelf).NotEmpty().GreaterThan(0);
            RuleForEach(x => x.Authors).SetValidator(new AuthorDTOValidator());
        }
    }
}
