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

namespace Application.Features.Githubs.Commands.UpdateGithub
{
    public class UpdateGithubCommand : IRequest<UpdateGithubDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? GithubUrl { get; set; }

        public class UpdateGithubCommandHandler : IRequestHandler<UpdateGithubCommand, UpdateGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _githubBusinessRules;

            public UpdateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules githubBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubBusinessRules = githubBusinessRules;
            }

            public async Task<UpdateGithubDto> Handle(UpdateGithubCommand request, CancellationToken cancellationToken)
            {
                await _githubBusinessRules.CheckDataForGithub(request.Id);
                Github mappedGithub = _mapper.Map<Github>(request);
                Github updatedGithub = await _githubRepository.UpdateAsync(mappedGithub);
                UpdateGithubDto updateGithubDto = _mapper.Map<UpdateGithubDto>(updatedGithub);
                return updateGithubDto;
            }
        }

        public string[] Roles { get; } = { "User" };
    }
}