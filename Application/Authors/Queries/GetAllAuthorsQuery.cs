﻿using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<List<Author>> { }
}
