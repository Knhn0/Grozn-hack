using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly Context _dbContext;

    public AuthorizationRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> RegisterAsync(Account account)
    {
        var acc = await _dbContext.Accounts.AddAsync(account);
        switch (account.UserInfo.Role.Title)
        {
            case "Student":
            {
                var student = new Student { User = acc.Entity.UserInfo };

                await _dbContext.Students.AddAsync(student);
                break;
            }
            case "Teacher":
            {
                var teacher = new Teacher { UserInfo = acc.Entity.UserInfo };

                await _dbContext.Teachers.AddAsync(teacher);
                break;
            }
        }

        await _dbContext.SaveChangesAsync();
        return acc.Entity;
    }

    public async Task<bool> CheckRegistration(Account account)
    {
        var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Username == account.Username);
        return acc == null;
    }

    public async Task<Account> Login(Account account)
    {
        var acc = await _dbContext.Accounts.Include(x => x.UserInfo).Include(x => x.UserInfo.Role).FirstOrDefaultAsync(x => x.Username == account.Username && x.Password == account
            .Password);
        return acc;
    }
}