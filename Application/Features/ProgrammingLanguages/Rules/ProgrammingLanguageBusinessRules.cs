using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task CheckForProgrammingLanguageWithSameName(string name)
        {
            IPaginate<ProgrammingLanguage> result =
                await _programmingLanguageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Aynı isimde programlama dili zaten var");
            }
        }

        public async Task CheckDataNull(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (result == null) throw new BusinessException("Böyle bir veri bulunmamaktadır");
        }
    }
}