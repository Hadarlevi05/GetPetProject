namespace GetPet.Common
{
    public static class StringExtensions
    {

        public static string ToHtmlText(this string text)
        {
            return text.Replace("\n", "<br/>");
        }
    }
}
