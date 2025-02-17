namespace MGM.Blog.Domain.Extensions
{
    internal static class StringExtensions
    {
        public static string OnlyDigits(this string value)
        {
            return new string(value.Where(char.IsDigit).ToArray());
        }
    }
}
