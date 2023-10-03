using Grozn.Domain.Entities;

namespace Grozn.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    void Update(User user);
}