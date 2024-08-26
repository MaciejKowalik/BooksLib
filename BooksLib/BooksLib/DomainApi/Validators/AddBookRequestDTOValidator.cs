using BooksLib.DomainApi.DTOs.AddBook;
using FluentValidation;

namespace BooksLib.DomainApi.Validators
{
    /// <summary>
    /// Validation class for validating AddBookRequestDTO values, using FluentValidation library
    /// </summary>
    public class AddBookRequestDTOValidator : AbstractValidator<AddBookRequestDTO>
    {
        public AddBookRequestDTOValidator()
        {
            RuleFor(x => x.Book).SetValidator(new BookDTOValidator());
        }
    }
}
