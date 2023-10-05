using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.Account;
using Contracts.Registraton;
using Domain.Entities;
using Jwt;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IRoleService _roleService;
    private readonly IJwtService _jwtService;

    public RegistrationService(IRegistrationRepository registrationRepository, IRoleService roleService, IJwtService jwtService)
    {
        _registrationRepository = registrationRepository;
        _roleService = roleService;
        _jwtService = jwtService;
    }

    public async Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        var role = await _roleService.GetRoleAsync(registrationRequestDto.Role);
        var refreshToken = _jwtService.CreateJwtToken(new List<Claim>(), 72);

        var account = new Account
        {
            Username = registrationRequestDto.Username,
            Password = registrationRequestDto.Password,

            UserInfo = new UserInfo
            {
                FirstName = registrationRequestDto.FirstName,
                SecondName = registrationRequestDto.SecondName,
                ThirdName = registrationRequestDto.ThirdName,
                Role = role
            },
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
            RefreshTokenExpiryTime = refreshToken.ValidTo
        };

        var accountReg = await _registrationRepository.RegisterAsync(account);
        var claims = new List<Claim> { new(ClaimTypes.Role, role.Title), new(ClaimTypes.UserData, accountReg.Id.ToString()) };


        var token = _jwtService.CreateJwtToken(claims);

        var response = new RegistrationResponseDto
        {
            Account = new AccountDto
            {
                Id = accountReg.Id,
                Username = accountReg.Username,
                UserInfo = new UserInfoDto
                {
                    Id = accountReg.UserInfo.Id,
                    FirstName = accountReg.UserInfo.FirstName,
                    SecondName = accountReg.UserInfo.SecondName,
                    ThirdName = accountReg.UserInfo.ThirdName,
                    Role = new RoleDto
                    {
                        Id = accountReg.UserInfo.Role.Id,
                        Title = accountReg.UserInfo.Role.Title
                    }
                }
            },
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpiryTime = token.ValidTo
        };

        return response;
    }
}