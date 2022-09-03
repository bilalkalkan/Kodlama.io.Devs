using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand : IRequest<CreateProgrammingLanguageDto>
    {
        public string Name { get; set; }

        public class CreateProgrammingLanguageHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreateProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public CreateProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreateProgrammingLanguageDto> Handle(
                CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.CheckForProgrammingLanguageWithSameName(request.Name);
                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);
                CreateProgrammingLanguageDto createProgrammingLanguageDto = _mapper.Map<CreateProgrammingLanguageDto>(createProgrammingLanguage);
                return createProgrammingLanguageDto;
            }
        }
    }
}