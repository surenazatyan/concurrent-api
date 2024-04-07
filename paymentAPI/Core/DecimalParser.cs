using System.Globalization;

namespace paymentAPI.Core;

public static class DecimalParser
{
    public static bool TryParseInvariant(string s, out decimal result)
    {
        // Try parsing with InvariantCulture
        if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            return true;
        }

        // Replace comma with dot and try parsing again
        s = s.Replace(',', '.');
        return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    }
}
