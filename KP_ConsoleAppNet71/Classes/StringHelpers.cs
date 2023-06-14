namespace KP_ConsoleAppNet71.Classes;

public class StringHelpers
{
    /// <summary>
    /// Increment upper-cased letter(s)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string IncrementLetter(string input)
    {
        List<char> chars = input.ToUpper().ToList();

        // Loop over the characters in the string, backwards
        for (int index = chars.Count - 1; index >= 0; index--)
        {
            if (chars[index] < 'A' || chars[index] > 'Z')
            {
                throw new ArgumentException("Input must contain only A-Z", nameof(input));
            }

            // Increment this character
            chars[index]++;

            if (chars[index] > 'Z')
            {
                // Oops, we overflowed past Z. Set it back to A, and ...
                chars[index] = 'A';

                // if this is the first character in the string, add a 'A' preceding it
                if (index == 0)
                {
                    chars.Add('A');
                }
                // otherwise we'll continue looping, and increment the next character on
                // the next loop iteration
            }
            else
            {
                // If we didn't overflow, we're done. Stop looping.
                break;
            }
        }

        return string.Concat(chars);
    }
}