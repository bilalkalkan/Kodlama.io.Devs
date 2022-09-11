using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Frameworks.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class FrameworkRepository : EfRepositoryBase<Framework, BaseDbContext>, IFrameworkRepository
    {
        public FrameworkRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<GetByIdFrameworkDto?> GetByIdFramework(Expression<Func<GetByIdFrameworkDto, bool>> predicate)
        {
            var result = from framework in Context.Frameworks
                         join programmingLanguage in Context.ProgrammingLanguages on framework.ProgrammingLanguageId equals
                             programmingLanguage.Id
                         select new GetByIdFrameworkDto
                         {
                             Id = framework.Id,
                             ProgrammingLanguageId = programmingLanguage.Id,
                             Name = framework.Name,
                             ProgrammingLanguageName = programmingLanguage.Name
                         };
            return result.SingleOrDefault(predicate);
        }
    }
}