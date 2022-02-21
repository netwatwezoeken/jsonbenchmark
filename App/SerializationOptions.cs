using Newtonsoft.Json;

namespace App;

public static class SerializationOptions
{
    public static readonly System.Text.Json.JsonSerializerOptions SystemTextJson = new()
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    public static readonly Newtonsoft.Json.JsonSerializerSettings Newtonsoft = new ()
    {
        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
        Converters = { new Newtonsoft.Json.Converters.StringEnumConverter()},
        StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
    };
}