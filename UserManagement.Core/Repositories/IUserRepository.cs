using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetByEmailAsync(string email);
}