using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Auths.Rules
{
    public class AuthBussinesRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBussinesRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserExistForRegister(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any())
            {
                throw new BusinessException("Bu email zaten kullanılıyor");
            }
        }

        public async Task UserExistForLogin(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result == null)
            {
                throw new BusinessException("Kullanıcı bulunamadı");
            }
        }
    }
}