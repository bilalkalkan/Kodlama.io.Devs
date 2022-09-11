using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Githubs.Queries.GetListGithub
{
    public class GetListGithubQuery : IRequest<GithubListModel>
    {
        public PageRequest? PageRequest { get; set; }

        public class GetListGithubQueryHandler : IRequestHandler<GetListGithubQuery, GithubListModel>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;

            public GetListGithubQueryHandler(IGithubRepository githubRepository, IMapper mapper)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
            }

            public async Task<GithubListModel> Handle(GetListGithubQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Github> github = await _githubRepository.GetListAsync(
                    include: f => f.Include(u => u.User),
                    size: request.PageRequest.PageSize, index: request.PageRequest.Page);
                GithubListModel githubListModel = _mapper.Map<GithubListModel>(github);
                return githubListModel;
            }
        }
    }
}