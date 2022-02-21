using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace App;

public record Book(
    string Author,
    CoverType CoverType, 
    int Isbn);

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
public enum CoverType
{
    Hard,
    Soft
}