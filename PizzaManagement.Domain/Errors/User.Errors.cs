using ErrorOr;

namespace PizzaManagement.Domain.Errors
{
    public partial class Errors
    {
        public static class Users
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "Users.DuplicateEmail",
                description: "The specified email address is already in use."
            );

            public static Error InvalidCredentials => Error.Unauthorized(
                code: "Users.InvalidCredentials",
                description: "Invalid credentials"
            );

            public static Error NotFound => Error.NotFound(
                code: "Users.NotFound",
                description: "User not found."
            );
        }
    }
}