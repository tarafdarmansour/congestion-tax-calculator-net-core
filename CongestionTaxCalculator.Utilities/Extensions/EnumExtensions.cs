namespace CongestionTaxCalculator.Utilities
{
    public static class EnumExtensions
    {
        public static List<string> ToList(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().Select(e => e.ToString()).ToList();
        }
    }
}