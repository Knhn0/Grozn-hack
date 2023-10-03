using System.Data;
using Dapper;
using Grozn.DAL.Interfaces;
using Grozn.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Grozn.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private string connectionString;

    public UserRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
    }

    internal IDbConnection Connection
    {
        get { return new NpgsqlConnection(connectionString); }
    }

    public async void CreateAsync(User item)
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            dbConnection.Execute(
                "INSERT INTO users (name, phone, email, login) VALUES (@Name, @Phone, @Email, @login)", item);
        }
    }

    public async Task<User?> GetAsync(int id)
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            return dbConnection.Query<User>("SELECT * FROM users WHERE id = @Id", new { Id = id }).FirstOrDefault();
        }
    }

    public IEnumerable<User> SelectAsync()
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            return dbConnection.Query<User>("SELECT * FROM users");
        }
    }

    public void DeleteAsync(int id)
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM users WHERE Id = @Id", new { Id = id });
        }
    }

    public void Update(User user)
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            dbConnection.Query(
                "UPDATE user SET name = @Name, phone = @Phone, email = @Email, login = @Login WHERE id = @Id", user);
        }
    }
}