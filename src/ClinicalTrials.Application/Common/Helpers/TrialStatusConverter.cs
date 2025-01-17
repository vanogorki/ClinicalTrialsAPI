using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ClinicalTrials.Application.Common.Helpers;

public sealed class TrialStatusConverter : StringEnumConverter
{
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var enumText = reader.Value!.ToString()!.Replace(" ", "");
        return Enum.Parse(objectType, enumText, true);
    }
}