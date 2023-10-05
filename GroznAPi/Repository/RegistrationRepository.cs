using Domain.Entities;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly Context _dbContext;

    public RegistrationRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> RegisterAsync(Account account)
    {
        var acc = await _dbContext.Accounts.AddAsync(entity: account);
        return acc.Entity;
    }
}