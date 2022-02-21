using System.Text.Json.Serialization;
namespace App;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Serialization | JsonSourceGenerationMode.Metadata,
    WriteIndented = false)]
[JsonSerializable(typeof(CoverType))] // Workaround in case an enum is nested, see https://github.com/dotnet/runtime/issues/61860
[JsonSerializable(typeof(Book))]
[JsonSerializable(typeof(List<Book>))]
public partial class JsonContext : JsonSerializerContext
{
}
