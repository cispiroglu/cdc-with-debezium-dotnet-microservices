namespace Shared.Common.Extensions;

public static class ConversionExtension
{
    public static List<KeyValuePair<string, object>> ToKeyValuePair(this object me) {
        var result = new List<KeyValuePair<string, object>>();
        foreach (var property in me.GetType().GetProperties()) {
            result.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(me)));
        }
        return result;
    }
    
    public static List<KeyValuePair<string, string>> ToKeyValuePairWithString(this object me) {
        var result = new List<KeyValuePair<string, string>>();
        foreach (var property in me.GetType().GetProperties()) {
            result.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(me)?.ToString()));
        }
        return result;
    }
    
    public static TEnum ToEnum<TEnum>(this int val) where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        if (!typeof(TEnum).IsEnum)
        {
            return default(TEnum);
        }

        if (Enum.IsDefined(typeof(TEnum), val))
        {//if a straightforward single value, return that
            return (TEnum)Enum.ToObject(typeof(TEnum), val);
        }

        var candidates = Enum
            .GetValues(typeof(TEnum))
            .Cast<int>()
            .ToList();

        var isBitwise = candidates
            .Select((n, i) =>
            {
                if (i < 2) return n == 0 || n == 1;
                return n / 2 == candidates[i - 1];
            })
            .All(y => y);

        var maxPossible = candidates.Sum();

        if (
            Enum.TryParse(val.ToString(), out TEnum asEnum)
            && (val <= maxPossible || !isBitwise)
        )
        {//if it can be parsed as a bitwise enum with multiple flags,
            //or is not bitwise, return the result of TryParse
            return asEnum;
        }

        //If the value is higher than all possible combinations,
        //remove the high imaginary values not accounted for in the enum
        var excess = Enumerable
            .Range(0, 32)
            .Select(n => (int)Math.Pow(2, n))
            .Where(n => n <= val && n > 0 && !candidates.Contains(n))
            .Sum();

        return Enum.TryParse((val - excess).ToString(), out asEnum) ? asEnum : default(TEnum);
    }
}