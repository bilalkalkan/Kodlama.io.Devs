using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Frameworks.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Commands.DeleteFramework
{
    public class DeleteFrameworkCommand : IRequest<DeleteFrameworkDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public class DeleteFrameworkHandler : IRequestHandler<DeleteFrameworkCommand, DeleteFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;

            public DeleteFrameworkHandler(IFrameworkRepository frameworkRepository, IMapper mapper)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
            }

            public async Task<DeleteFrameworkDto> Handle(DeleteFrameworkCommand request, CancellationToken cancellationToken)
            {
                Framework mappedframework = _mapper.Map<Framework>(request);
                Framework deletedFramework = await _frameworkRepository.DeleteAsync(mappedframework);
                DeleteFrameworkDto deleteFrameworkDto = _mapper.Map<DeleteFrameworkDto>(deletedFramework);
                return deleteFrameworkDto;
            }
        }

        public string[] Roles { get; } = { "Admin", "User" };
    }
}