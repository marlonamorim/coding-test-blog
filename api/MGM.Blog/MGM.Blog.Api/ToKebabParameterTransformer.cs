using System.Text.RegularExpressions;

namespace MGM.Blog.Api
{
    public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value) => value != null
            ? RegexKebabCase().Replace(value.ToString()!, "$1-$2").ToLower()
            : null;

        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex RegexKebabCase();
    }
}
