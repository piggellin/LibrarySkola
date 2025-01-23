using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Result<BookDto>>
    {
        public BookDto Book { get; set; }

        public UpdateBookCommand(BookDto book)
        {
            Book = book;
        }
    }
}
