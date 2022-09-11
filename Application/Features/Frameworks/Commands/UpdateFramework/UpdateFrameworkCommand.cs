using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Commands.UpdateFramework
{
    public class UpdateFrameworkCommand : IRequest<UpdateFrameworkDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string? Name { get; set; }

        public class UpdateFrameworkCommandHandler : IRequestHandler<UpdateFrameworkCommand, UpdateFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;
            private readonly FrameworkBusinessRules _frameworkBusinessRules;

            public UpdateFrameworkCommandHandler(IFrameworkRepository frameworkRepository, IMapper mapper, FrameworkBusinessRules frameworkBusinessRules)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
                _frameworkBusinessRules = frameworkBusinessRules;
            }

            public async Task<UpdateFrameworkDto> Handle(UpdateFrameworkCommand request, CancellationToken cancellationToken)
            {
                await _frameworkBusinessRules.CheckForFrameworkWithId(request.Id);
                Framework mappedFramework = _mapper.Map<Framework>(request);
                Framework updatedFramework = await _frameworkRepository.UpdateAsync(mappedFramework);
                UpdateFrameworkDto updateFrameworkDto = _mapper.Map<UpdateFrameworkDto>(updatedFramework);
                return updateFrameworkDto;
            }
        }

        public string[] Roles { get; } = { "Admin", "User" };
    }
}