using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Frameworks.Dtos;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IFrameworkRepository : IAsyncRepository<Framework>, IRepository<Framework>
    {
        Task<GetByIdFrameworkDto?> GetByIdFramework(Expression<Func<GetByIdFrameworkDto, bool>> predicate);
    }
}