using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.LoginCommand
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AuthenticatorCode { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBussinesRules _authBussinesRules;

            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBussinesRules authBussinesRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authBussinesRules = authBussinesRules;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? result = await _userRepository.GetAsync(b => b.Email == request.Email);
                if (result == null)
                {
                    throw new BusinessException("Kullanıcı Bulunamadı");
                }

                if (!HashingHelper.VerifyPasswordHash(request.Password, result.PasswordHash, result.PasswordSalt))
                {
                    throw new BusinessException("Parola hatası");
                }
                var claims = _userRepository.GetClaims(result);
                AccessToken accessToken = _tokenHelper.CreateToken(result, claims);
                return accessToken;
            }
        }
    }
}