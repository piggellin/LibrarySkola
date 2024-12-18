﻿using Domain.Models;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Result<Book>>
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
    }
}
