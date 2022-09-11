using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Frameworks.Commands.UpdateFramework
{
    public class UpdateFrameworkCommandValidator : AbstractValidator<UpdateFrameworkCommand>
    {
        public UpdateFrameworkCommandValidator()
        {
            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Name).MinimumLength(2);
            RuleFor(f => f.ProgrammingLanguageId).NotEmpty();
        }
    }
}