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
        var acc = await _dbContext.Accounts.AddAsync(account);
        switch (account.UserInfo.Role.Title)
        {
            case "Student":
            {
                var student = new Student {User = acc.Entity.UserInfo};
            
                await _dbContext.Students.AddAsync(student);
                break;
            }
            case "Teacher":
            {
                var teacher = new Teacher {UserInfo = acc.Entity.UserInfo};
            
                await _dbContext.Teachers.AddAsync(teacher);
                break;
            }
        }

        await _dbContext.SaveChangesAsync();
        return acc.Entity;
    }
}