using BooksLib.Domain.Models;
using FluentValidation;

namespace BooksLib.DomainApi.Validators
{
    /// <summary>
    /// Validation class for validating AuthorDTO values, using FluentValidation library
    /// </summary>
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
