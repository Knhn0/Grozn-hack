using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class UserRepository : IUserRepository
{
    private readonly Context _db;

    public UserRepository(Context db)
    {
        _db = db;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        var result = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new Exception("User not found");
    }

    public async Task<Account> UpdateAsync(Account t)
    {
        var u = await _db.Users.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (u == null)
        {
            throw new Exception("User not found"); // todo
        }
        u.Email = t.Email;
        u.Username = t.Username;
        u.Phone = t.Phone;
        u.Password = new PasswordHasher().HashPassword(t.Password);
        
        await _db.SaveChangesAsync();
        return u;
    }

    public async Task CreateAsync(Account t)
    {
        t.Password = new PasswordHasher().HashPassword(t.Password);
        await _db.Users.AddAsync(t);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account t)
    {
        _db.Users.Remove(t); //may not work
        await _db.SaveChangesAsync();
    }
}