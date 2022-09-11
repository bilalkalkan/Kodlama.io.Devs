using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Frameworks.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Frameworks.Queries.GetListFramework
{
    public class GetListFrameworkQuery : IRequest<FrameworkListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }

        public class GetListFrameworkQueryHandler : IRequestHandler<GetListFrameworkQuery, FrameworkListModel>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;

            public GetListFrameworkQueryHandler(IFrameworkRepository frameworkRepository, IMapper mapper)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
            }

            public async Task<FrameworkListModel> Handle(GetListFrameworkQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Framework> frameworks = await _frameworkRepository.GetListAsync(
                    include: f => f.Include(p => p.ProgrammingLanguage), index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                FrameworkListModel mapperModel = _mapper.Map<FrameworkListModel>(frameworks);
                return mapperModel;
            }
        }

        public string[] Roles { get; } = { "User" };
    }
}