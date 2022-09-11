using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Queries.GetByIdFramework
{
    public class GetByIdFrameworkQuery : IRequest<GetByIdFrameworkDto>
    {
        public int Id { get; set; }

        public class FrameworkGetByIdQueryHandler : IRequestHandler<GetByIdFrameworkQuery, GetByIdFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;
            private readonly FrameworkBusinessRules _businessRules;

            public FrameworkGetByIdQueryHandler(IFrameworkRepository frameworkRepository, IMapper mapper, FrameworkBusinessRules businessRules)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<GetByIdFrameworkDto> Handle(GetByIdFrameworkQuery request, CancellationToken cancellationToken)
            {
                Framework? framework = _mapper.Map<Framework>(request);
                await _businessRules.CheckForFrameworkWithId(request.Id);
                GetByIdFrameworkDto? frameworkDto =
                    await _frameworkRepository.GetByIdFramework(f => f.Id == framework.Id);
                return frameworkDto;
            }
        }
    }
}