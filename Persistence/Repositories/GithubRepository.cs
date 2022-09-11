using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class GithubRepository : EfRepositoryBase<Github, BaseDbContext>, IGithubRepository
    {
        public GithubRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<GetByIdGithubDto?> GetByIdFramework(Expression<Func<GetByIdGithubDto, bool>> predicate)
        {
            var result = from github in Context.Githubs
                         join user in Context.Users on github.UserId equals user.Id
                         select new GetByIdGithubDto
                         {
                             Id = github.Id,
                             UserId = user.Id,
                             UserFullName = user.FirstName + " " + user.LastName,
                             GithubUrl = github.GithubUrl,
                         };
            return result.FirstOrDefault(predicate);
        }
    }
}