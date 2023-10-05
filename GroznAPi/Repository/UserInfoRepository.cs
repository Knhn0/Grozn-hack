using Domain.Entities;
using Exceptions.Implementation;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Repository.Abstractions;

namespace Repository;

public class UserInfoRepository : IUserInfoRepository
{
    private readonly Context _db;
    private readonly IStudentRepository _studentRepository;

    public UserInfoRepository(Context db, IStudentRepository studentRepository)
    {
        _db = db;
        _studentRepository = studentRepository;
    }

    public async Task<List<UserInfo>> GetAllAsync()
    {
        return await _db.UserInfos.ToListAsync();
    }

    public async Task<UserInfo> GetByIdAsync(int id)
    {
        var result = await _db.UserInfos.FirstOrDefaultAsync(x => x.Id == id);
        return result ?? throw new CourseNotFoundException("UserInfo not found");
    }

    public async Task<UserInfo> UpdateAsync(UserInfo t)
    {
        var dbInfo = await _db.UserInfos.FirstOrDefaultAsync(x => x.Id == t.Id);
        if (dbInfo == null)
        {
            throw new CourseNotFoundException("UserInfo not found");
        }

        dbInfo.FirstName = t.FirstName;
        dbInfo.SecondName = t.SecondName;
        dbInfo.ThirdName = t.ThirdName;
        dbInfo.Role = t.Role;
            
        await _db.SaveChangesAsync();
        return dbInfo;
    }

    public async Task<UserInfo> CreateAsync(UserInfo t)
    {
        var result = await _db.UserInfos.AddAsync(t);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(UserInfo t)
    {
        var result = _db.UserInfos.Remove(t);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        var result = await GetByIdAsync(id);
        return result.Role;
    }

    public async Task<Student> GetStudentAsync(int id)
    {
        var result = await _studentRepository.GetByUserIdAsync(id);
        return result ?? throw new StudentNotFoundException("Student not found");
    }
}