using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Contracts.Account;
using Contracts.Autorization;
using Contracts.Registraton;
using Domain.Entities;
using Repository.Abstractions;
using Service.Abstactions;

namespace Service;

public class AuthorizationService : IAuthorizationService
{
    private readonly IAuthorizationRepository _authorizationRepository;
    private readonly IRoleService _roleService;
    private readonly IJwtService _jwtService;

    public AuthorizationService(IAuthorizationRepository authorizationRepository, IRoleService roleService, IJwtService jwtService)
    {
        _authorizationRepository = authorizationRepository;
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
        if (!await _authorizationRepository.CheckRegistration(account))
        {
            throw new Exception("This login is already taken");
        }

        var accountReg = await _authorizationRepository.RegisterAsync(account);
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

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        if (loginRequestDto == null)
        {
            throw new Exception("Invalid credentials");
        }

        var account = await _authorizationRepository.Login(new Account
        {
            Username = loginRequestDto.UserName,
            Password = loginRequestDto.Password
        });

        var claims = new List<Claim> { new(ClaimTypes.Role, account.UserInfo.Role.Title), new(ClaimTypes.UserData, account.Id.ToString()) };


        var token = _jwtService.CreateJwtToken(claims);


        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpiryTime = token.ValidTo
        };
    }
}