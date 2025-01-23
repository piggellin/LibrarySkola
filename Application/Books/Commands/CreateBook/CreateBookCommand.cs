using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Result<BookDto>>
    {
        public BookDto Book { get; set; }

        public CreateBookCommand(BookDto book)
        {
            Book = book;
        }
    }
}
