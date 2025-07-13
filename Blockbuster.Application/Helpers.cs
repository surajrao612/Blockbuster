namespace Blockbuster.Application
{
    public static class Helpers
    {
        public static decimal ConvertToDecimal(this string? price)
        {
            if (string.IsNullOrEmpty(price))
                return 0;

            else return decimal.Parse(price);
        }
    }
}
