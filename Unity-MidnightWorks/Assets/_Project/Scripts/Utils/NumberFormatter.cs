namespace CityBuilder
{
    public static class NumberFormatter
    {
        public static string FormatNumber(this int value)
        {
            if (value < 1000)
                return value.ToString("0");

            if (value < 1_000_000)
                return (value / 1000f).ToString("0.#") + "K";

            return (value / 1_000_000f).ToString("0.#") + "M";
        }
    }
}