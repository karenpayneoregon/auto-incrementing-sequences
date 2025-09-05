namespace UtilitiesLibrary;

public static partial class StringExtensions
{
    // Compiled at build-time for performance, avoids reparsing the regex every call
    [GeneratedRegex(@"([A-Z][a-z]+)")]
    private static partial Regex CamelCasePattern();

    /// <summary>
    /// Splits a camel-cased string on upper-cased characters and separates with a single space.
    /// </summary>
    public static string SplitCamelCase(this string sender) =>
        string.Join(" ", CamelCasePattern().Matches(sender)
            .Select(m => m.Value));
}