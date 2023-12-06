using System.ComponentModel.DataAnnotations;

namespace PizzaManagement.Domain.Entities.Recipes.Enums
{
    public enum TimeMeasurement
    {
        [Display(Name = "Second(s)")]
        Seconds,

        [Display(Name = "Minute(s)")]
        Minutes,

        [Display(Name = "Hour(s)")]
        Hours,

        [Display(Name = "Day(s)")]
        Days
    }
}