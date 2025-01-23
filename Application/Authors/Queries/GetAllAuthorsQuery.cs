﻿using Application.DTOs;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<Result<List<AuthorDto>>> { }
}
