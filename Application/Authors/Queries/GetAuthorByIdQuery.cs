﻿using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<Result<AuthorDto>>
    {
        public int AuthorId { get; set; }

        public GetAuthorByIdQuery(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
