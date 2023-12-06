using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Primitives;

namespace PizzaManagement.Domain.Entities.Users
{
    public class User : Entity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public List<Rating> Ratings { get; set; } = null!;
        public User()
        {
        }

        public User(Guid id, string firstName, string lastName, string email, string passwordHash) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}