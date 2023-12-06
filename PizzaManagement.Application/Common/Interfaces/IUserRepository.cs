using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Application.Common.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string? email);
    }
}