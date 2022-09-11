using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Frameworks.Commands.CreateFamework
{
    public class CreateFrameworkCommandValidator : AbstractValidator<CreateFrameworkCommand>
    {
        public CreateFrameworkCommandValidator()
        {
            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Name).MinimumLength(2);
            RuleFor(f => f.ProgrammingLanguageId).NotEmpty();
        }
    }
}