using BookShop.Infra;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookShop.WebUi.Mediator.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly AppDbContext _context;
        public CreateBookCommandValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(a => a.Isbn)
               .Cascade(CascadeMode.Stop)
               .Length(13).WithMessage("Isbn 13 character.")
               .Must((x) =>
               {
                   return string.IsNullOrEmpty(x) || ! _context.Books.Any(a=>a.Isbn == x);
               }).WithMessage(a=>$"Check Isbn is duplicate.");

            //RuleFor(a => a.CoverPrice)
            //    .Cascade(CascadeMode.Stop)
            //    .GreaterThan(0).WithMessage("");
        }
    }
}
