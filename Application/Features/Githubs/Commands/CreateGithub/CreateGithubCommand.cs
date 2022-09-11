using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Githubs.Commands.CreateGithub
{
    public class CreateGithubCommand : IRequest<CreateGithubDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? GithubUrl { get; set; }

        public class CreateGithubCommandHandler : IRequestHandler<CreateGithubCommand, CreateGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _githubBusinessRules;

            public CreateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules githubBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubBusinessRules = githubBusinessRules;
            }

            public async Task<CreateGithubDto> Handle(CreateGithubCommand request, CancellationToken cancellationToken)
            {
                if (request.GithubUrl != null)
                    await _githubBusinessRules.CheckForGithubUrlWithSameUrl(request.GithubUrl);
                Github mappedGithub = _mapper.Map<Github>(request);
                Github createdGithub = await _githubRepository.AddAsync(mappedGithub);
                CreateGithubDto createGithubDto = _mapper.Map<CreateGithubDto>(createdGithub);
                return createGithubDto;
            }
        }

        public string[] Roles { get; } = { "User" };
    }
}