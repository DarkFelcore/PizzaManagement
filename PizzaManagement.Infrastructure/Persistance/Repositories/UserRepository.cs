using Microsoft.EntityFrameworkCore;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}