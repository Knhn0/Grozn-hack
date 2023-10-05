using Domain.Entities;
using Exceptions.Implementation;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class AccountRepository : IAccountRepository
{
    private readonly Context _db;

    public AccountRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _db.Accounts.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        var result = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new AccountNotFoundException("Account not found");
    }

    public async Task<Account> UpdateAsync(Account t)
    {
        var u = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (u == null)
        {
            throw new AccountNotFoundException("Account not found");
        }
        //u.Email = t.Email;
        u.Username = t.Username;
        //u.Phone = t.Phone;
        u.Password = new PasswordHasher().HashPassword(t.Password);
        
        await _db.SaveChangesAsync();
        return u;
    }

    public async Task CreateAsync(Account t)
    {
        t.Password = new PasswordHasher().HashPassword(t.Password);
        await _db.Accounts.AddAsync(t);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account t)
    {
        _db.Accounts.Remove(t); //may not work
        await _db.SaveChangesAsync();
    }
}