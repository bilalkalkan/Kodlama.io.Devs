﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Frameworks.Dtos
{
    public class CreateFrameworkDto
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string? Name { get; set; }
    }
}