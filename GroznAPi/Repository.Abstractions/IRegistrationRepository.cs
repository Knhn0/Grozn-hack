using Domain.Entities;

namespace Repository.Abstractions;

public interface IRegistrationRepository
{
    Task<Account> RegisterAsync(Account account);
}