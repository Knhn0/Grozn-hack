using Domain.Entities;

namespace Repository.Abstractions;

public interface IAuthorizationRepository
{
    Task<Account> RegisterAsync(Account account);
    Task<bool> CheckRegistration(Account account);
    Task<Account> Login(Account account);
}