using Bogus;
using App;
using Xunit;

namespace Test;

public class Deserialization
{
    private readonly string _json;

    public Deserialization()
    {
        Faker<Book> faker = new();
        
        var book = faker.CustomInstantiator(f =>
            new Book(
                f.Name.FirstName(),
                f.PickRandom<CoverType>(),
                f.Random.Int()
            )).Generate();
        
        _json = Newtonsoft.Json.JsonConvert.SerializeObject(book, SerializationOptions.Newtonsoft);
    }
    
    [Fact]
    public void NewtonsoftEqualsSystemText()
    {
        var newton = Newtonsoft.Json.JsonConvert.DeserializeObject<Book>(_json, SerializationOptions.Newtonsoft);
        var system = System.Text.Json.JsonSerializer.Deserialize<Book>(_json, SerializationOptions.SystemTextJson);
        
        Assert.Equal(newton, system);
    }
    
    [Fact]
    public void NewtonsoftEqualsGeneratedSystemText()
    {
        var newton = Newtonsoft.Json.JsonConvert.DeserializeObject<Book>(_json, SerializationOptions.Newtonsoft);
        var generated = System.Text.Json.JsonSerializer.Deserialize(_json, JsonContext.Default.Book);
        
        Assert.Equal(newton, generated);
    }
}