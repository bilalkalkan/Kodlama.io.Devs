using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Githubs.Queries.GetByIdGithub
{
    public class GetByIdGithubQuery : IRequest<GetByIdGithubDto>
    {
        public int Id { get; set; }

        public class GetByIdGithubQueryHandler : IRequestHandler<GetByIdGithubQuery, GetByIdGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _businessRules;

            public GetByIdGithubQueryHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<GetByIdGithubDto> Handle(GetByIdGithubQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.CheckDataForGithub(request.Id);

                Github? mappedGithub = _mapper.Map<Github>(request);

                GetByIdGithubDto? getByIdGithubDto = await _githubRepository.GetByIdFramework(z => z.Id == mappedGithub.Id);
                return getByIdGithubDto;
            }
        }
    }
}