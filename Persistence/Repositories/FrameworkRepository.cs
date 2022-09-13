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
    }
}