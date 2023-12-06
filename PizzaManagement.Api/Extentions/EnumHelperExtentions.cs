using System.ComponentModel.DataAnnotations;

namespace PizzaManagement.Api.Extentions
{
    public static class EnumHelperExtentions
    {
        public static string GetEnumDisplayName(Enum value)
        {
            var displayNameAttribute = value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayNameAttribute?.Name ?? value.ToString();
        } 
    }
}