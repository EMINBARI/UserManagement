using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Abstractions;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class UserRepository(PostgresContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Value == email);
        
        return user;
    }
}