using PizzaManagement.Application.Common.Interfaces;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}