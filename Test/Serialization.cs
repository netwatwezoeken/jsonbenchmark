using Bogus;
using App;
using Xunit;

namespace Test;

public class Serialization
{
    private readonly Book _book;

    public Serialization()
    {
        Faker<Book> faker = new();
        _book = faker.CustomInstantiator(f =>
            new Book(
                f.Name.FirstName(),
                f.PickRandom<CoverType>(),
                f.Random.Int()
            )).Generate();
    }
    
    [Fact]
    public void NewtonsoftEqualsSystemText()
    {
        var newton = Newtonsoft.Json.JsonConvert.SerializeObject(_book, SerializationOptions.Newtonsoft);
        var system = System.Text.Json.JsonSerializer.Serialize(_book, SerializationOptions.SystemTextJson);
        
        Assert.Equal(newton, system);
    }
    
    [Fact]
    public void NewtonsoftEqualsGeneratedSystemText()
    {
        var newton = Newtonsoft.Json.JsonConvert.SerializeObject(_book, SerializationOptions.Newtonsoft);
        var generated = System.Text.Json.JsonSerializer.Serialize(_book, JsonContext.Default.Book);
        
        Assert.Equal(newton, System.Text.RegularExpressions.Regex.Unescape(generated));
    }
}