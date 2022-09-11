using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Githubs.Rules
{
    public class GithubBusinessRules
    {
        private readonly IGithubRepository _githubRepository;

        public GithubBusinessRules(IGithubRepository githubRepository)
        {
            _githubRepository = githubRepository;
        }

        public async Task CheckForGithubUrlWithSameUrl(string githubUrl)
        {
            var result = await _githubRepository.GetListAsync(b => b.GithubUrl == githubUrl);
            if (result.Items.Any())
            {
                throw new BusinessException("Aynı adrese sahip github zaten mevcut ");
            }
        }

        public async Task CheckDataForGithub(int id)
        {
            var result = await _githubRepository.GetAsync(b => b.Id == id);
            if (result == null)
            {
                throw new BusinessException("Böyle bir veri bulunmamaktadır");
            }
        }
    }
}