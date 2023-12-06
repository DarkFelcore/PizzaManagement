using PizzaManagement.Domain.Entities.Recipes.Enums;

namespace PizzaManagement.Domain.Entities.Recipes.ValueObjects
{
    public class PreparationTime
    {
        public int Duration { get; private set; }
        public TimeMeasurement TimeMeasurement { get; private set; }
        public PreparationTime(int duration, TimeMeasurement timeMeasurement)
        {
            Duration = duration;
            TimeMeasurement = timeMeasurement;
        }
    }
}