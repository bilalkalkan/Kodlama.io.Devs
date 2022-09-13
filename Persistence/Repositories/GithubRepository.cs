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
    }
}