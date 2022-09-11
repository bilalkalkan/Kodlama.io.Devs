using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Frameworks.Rules
{
    public class FrameworkBusinessRules
    {
        private readonly IFrameworkRepository _frameworkRepository;

        public FrameworkBusinessRules(IFrameworkRepository frameworkRepository)
        {
            _frameworkRepository = frameworkRepository;
        }

        public async Task CheckForFrameworkWithSameName(string name)
        {
            IPaginate<Framework> result = await _frameworkRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Bu isimde zaten framework var");
            }
        }

        public async Task CheckForFrameworkWithId(int id)
        {
            Framework? resut = await _frameworkRepository.GetAsync(f => f.Id == id);
            if (resut == null)
            {
                throw new BusinessException("Böyle bir veri bulunmamaktadır");
            }
        }
    }
}