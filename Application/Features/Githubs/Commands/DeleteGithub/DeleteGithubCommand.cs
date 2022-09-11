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

namespace Application.Features.Githubs.Commands.DeleteGithub
{
    public class DeleteGithubCommand : IRequest<DeleteGithubDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public class DeleteGithubCommandHandler : IRequestHandler<DeleteGithubCommand, DeleteGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _businessRules;

            public DeleteGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<DeleteGithubDto> Handle(DeleteGithubCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.CheckDataForGithub(request.Id);
                Github mappedGithub = _mapper.Map<Github>(request);
                Github deletedGithub = await _githubRepository.DeleteAsync(mappedGithub);
                DeleteGithubDto deleteGithubDto = _mapper.Map<DeleteGithubDto>(deletedGithub);
                return deleteGithubDto;
            }
        }

        public string[] Roles { get; } = { "User" };
    }
}