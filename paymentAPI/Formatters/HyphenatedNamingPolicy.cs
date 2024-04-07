using System.Text.Json;
using System.Text.RegularExpressions;

namespace paymentAPI.Formatters;

// Custom JsonNamingPolicy
public class HyphenatedNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // Logic to insert hyphens before capital letters and make lowercase
        return Regex.Replace(name, "(?<!^)([A-Z])", "-$1").ToLower();
    }
}
