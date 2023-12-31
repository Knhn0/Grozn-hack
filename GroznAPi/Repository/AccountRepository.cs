﻿using Domain.Entities;
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
        return await _db.Accounts
            .Include(x => x.UserInfo)
            .Include(x => x.UserInfo.Role).ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        var result = await _db.Accounts
            .Include(x => x.UserInfo)
            .Include(x => x.UserInfo.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new AccountNotFoundException("Account not found");
    }

    public async Task<Account> UpdateAsync(Account t)
    {
        var u = await GetByIdAsync(t.Id);
        
        //u.Email = t.Email;
        u.Username = t.Username;
        //u.Phone = t.Phone;
        u.Password = new PasswordHasher().HashPassword(t.Password);
        
        await _db.SaveChangesAsync();
        return u;
    }

    public async Task<Account> CreateAsync(Account t)
    {
        t.Password = new PasswordHasher().HashPassword(t.Password);
        var res = await _db.Accounts.AddAsync(t);
        await _db.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<bool> DeleteAsync(Account t)
    {
        var result = _db.Accounts.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }
}