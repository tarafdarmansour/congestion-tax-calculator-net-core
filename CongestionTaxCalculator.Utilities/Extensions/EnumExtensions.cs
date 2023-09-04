using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CongestionTaxCalculator.Utilities
{
    public static class EnumExtensions
    {
        public static List<string> ToList(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().Select(e => e.ToString()).ToList();
        }
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }
        public enum DisplayProperty
        {
            Description,
            GroupName,
            Name,
            Prompt,
            ShortName,
            Order
        }
    }
}