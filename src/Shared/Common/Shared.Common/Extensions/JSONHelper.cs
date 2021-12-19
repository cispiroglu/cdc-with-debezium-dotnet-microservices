using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shared.Common.Extensions;

public static class JSONHelper
{
    public static string ToJSON(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
        
    public static T ToObject<T>(this string str)
    {
        return JsonConvert.DeserializeObject<T>(str);
    }

    public static bool IsValidJson(this string stringValue)
    {
        if (string.IsNullOrWhiteSpace(stringValue))
            return false;

        var value = stringValue.Trim();

        if ((!value.StartsWith("{") || !value.EndsWith("}")) &&
            (!value.StartsWith("[") || !value.EndsWith("]"))) 
            return false;
            
        try
        {
            var obj = JToken.Parse(value);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}