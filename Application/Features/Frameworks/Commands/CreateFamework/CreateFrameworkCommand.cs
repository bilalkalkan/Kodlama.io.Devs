using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Commands.CreateFamework
{
    public class CreateFrameworkCommand : IRequest<CreateFrameworkDto>, ISecuredRequest
    {
        public CreateFrameworkCommand(int programmingLanguageId, string name)
        {
            ProgrammingLanguageId = programmingLanguageId;
            Name = name;
        }

        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateFrameworkCommandHandler : IRequestHandler<CreateFrameworkCommand, CreateFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;
            private readonly FrameworkBusinessRules _frameworkBusinessRules;

            public CreateFrameworkCommandHandler(IFrameworkRepository frameworkRepository, IMapper mapper, FrameworkBusinessRules frameworkBusinessRules)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
                _frameworkBusinessRules = frameworkBusinessRules;
            }

            public async Task<CreateFrameworkDto> Handle(CreateFrameworkCommand request, CancellationToken cancellationToken)
            {
                await _frameworkBusinessRules.CheckForFrameworkWithSameName(request.Name);
                Framework framework = _mapper.Map<Framework>(request);
                Framework mappedFramework = await _frameworkRepository.AddAsync(framework);
                CreateFrameworkDto createFrameworkDto = _mapper.Map<CreateFrameworkDto>(mappedFramework);
                return createFrameworkDto;
            }
        }

        public string[] Roles { get; } = { "Admin", "User" };
    }
}