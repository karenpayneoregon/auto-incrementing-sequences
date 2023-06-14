namespace SequenceLibrary;

public class Helpers
{
    /// <summary>
    /// Given a string which ends with a number, increment the number by 1
    /// </summary>
    /// <param name="sender">string ending with a number</param>
    /// <returns>string with ending number incremented by 1</returns>
    public static string NextValue(string sender)
    {
        string value = Regex.Match(sender, "[0-9]+$").Value;
        return sender[..^value.Length] + (long.Parse(value) + 1)
            .ToString().PadLeft(value.Length, '0');
    }

    /// <summary>
    /// Wrapper for NextValue as some may like this name
    /// </summary>
    public static string NextInvoiceNumber(string sender) => NextValue(sender);


    /// <summary>
    /// Given a string which ends with a number, increment the number by  <seealso cref="incrementBy"/>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="incrementBy">increment by</param>
    /// <returns>string with ending number incremented by <seealso cref="incrementBy"/></returns>
    public static string NextValue(string sender, int incrementBy)
    {
        string value = Regex.Match(sender, "[0-9]+$").Value;
        return sender[..^value.Length] + (long.Parse(value) + incrementBy)
            .ToString().PadLeft(value.Length, '0');
    }

    /// <summary>
    /// Wrapper for NextValue as some may lik this name
    /// </summary>
    public static string NextInvoiceNumber(string sender, int incrementBy) => NextValue(sender, incrementBy);

    /// <summary>
    /// Simple way to create auto incrementing alpha numeric string
    /// </summary>
    /// <param name="letter">Prefix</param>
    /// <param name="identifier">value to use</param>
    /// <returns>alpha numeric string</returns>
    public static string NextValueFromIdentifier(char letter, int identifier)
    {
        if (!Characters.Contains(char.ToUpper(letter)))
        {
            return "";
        }

        int lead = char.ConvertToUtf32(char.ToString(letter).ToUpper(), 0);

        var parts = new List<string>();
        int numberPart = identifier % 10000;

        parts.Add(numberPart.ToString("0000"));
        identifier /= 10000;

        for (int index = 0; index < 3 || identifier > 0; ++index)
        {
            parts.Add(((char)(lead + (identifier % 26))).ToString());
            identifier /= 26;
        }

        return string.Join(string.Empty, parts.AsEnumerable().Reverse().ToArray());
    }

    public static char[] Characters => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
}