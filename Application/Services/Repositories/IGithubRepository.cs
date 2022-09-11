using Application.Features.Frameworks.Dtos;
using Core.Persistence.Repositories;
using Domain.Entities;
using System.Linq.Expressions;
using Application.Features.Githubs.Dtos;

namespace Application.Services.Repositories
{
    public interface IGithubRepository : IAsyncRepository<Github>, IRepository<Github>
    {
        Task<GetByIdGithubDto?> GetByIdFramework(Expression<Func<GetByIdGithubDto, bool>> predicate);
    }
}