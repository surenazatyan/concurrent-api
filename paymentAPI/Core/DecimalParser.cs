using System.Globalization;

namespace paymentAPI.Core;

public static class DecimalParser
{
    public static bool TryParseInvariant(string s, out decimal result)
    {
        if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            return true;
        }

        s = s.Replace(',', '.');
        return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
    }
}
