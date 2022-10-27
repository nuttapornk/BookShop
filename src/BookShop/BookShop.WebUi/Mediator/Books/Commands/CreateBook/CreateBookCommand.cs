using BookShop.Infra;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookShop.WebUi.Mediator.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Unit>
    {
        [Required,StringLength(100),Display(Name = "ชื่อภาษาไทย")]
        public string NameThai { get; set; }

        [StringLength(100),Display(Name = "ชื่อภาษาอังกฤษ")]
        public string NameEng { get; set; }

        [StringLength(13)]
        public string Isbn { get; set; }
        
        public decimal? CoverPrice { get; set; }
        
        public string Author { get; set; }

        public string Abstract { get; set; }

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public bool Status { get; set; }

        public class CreateBookCommadHanler : IRequestHandler<CreateBookCommand, Unit>
        {
            private readonly AppDbContext _context;
            public CreateBookCommadHanler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                await _context.Books.AddAsync(new Infra.Models.Book
                {
                    NameEng = request.NameEng,
                    NameThai = request.NameThai,
                    Isbn = request.Isbn,
                    CoverPrice = request.CoverPrice,
                    Status = request.Status ? 1: 0,
                    Author = request.Author,
                    Abstract = request.Abstract,
                    PublisherId = request.PublisherId,
                    UserInsert = "_",
                    UserUpdate = "_"                    
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
