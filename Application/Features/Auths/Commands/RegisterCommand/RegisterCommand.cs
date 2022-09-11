using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMapper _mapper;
            private readonly AuthBussinesRules _authBussinesRules;

            public RegisterCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMapper mapper, AuthBussinesRules authBussinesRules)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
                _authBussinesRules = authBussinesRules;
            }

            public async Task<AccessToken> CreateAccessToken(User user)
            {
                var claims = _userRepository.GetClaims(user);
                var token = _tokenHelper.CreateToken(user, claims);
                return token;
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBussinesRules.UserExistForRegister(request.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Status = true,
                };

                User mappedUser = _mapper.Map<User>(user);
                User createdUser = await _userRepository.AddAsync(mappedUser);
                UserForRegisterDto userForRegisterDto = _mapper.Map<UserForRegisterDto>(createdUser);
                var claims = _userRepository.GetClaims(user);
                AccessToken accessToken = _tokenHelper.CreateToken(createdUser, claims);
                return accessToken;
            }
        }
    }
}