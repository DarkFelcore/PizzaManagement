using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}