﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateBookCommand
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
    }
}
