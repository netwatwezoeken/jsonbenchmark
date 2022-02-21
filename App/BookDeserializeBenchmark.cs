#nullable disable
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;

namespace App;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class BookDeserializeBenchmark
{
    private List<Book> _books = new();
    private readonly Newtonsoft.Json.JsonSerializerSettings _newtonOptions = SerializationOptions.Newtonsoft;
    private readonly System.Text.Json.JsonSerializerOptions _systemOptions = SerializationOptions.SystemTextJson;
    private string _bookAsText;
    private string _booksAsText;

    [GlobalSetup]
    public void Setup()
    {
        Faker<Book> faker = new();
        _books = faker.CustomInstantiator(f =>
            new Book(
                f.Name.FirstName(),
                f.PickRandom<CoverType>(),
                f.Random.Int()
            )).Generate(1000);
        _booksAsText = System.Text.Json.JsonSerializer.Serialize(_books, _systemOptions);
        _bookAsText = System.Text.Json.JsonSerializer.Serialize(_books[0], _systemOptions);
    }
    
    [BenchmarkCategory("List"), Benchmark]
    public List<Book> NewtonSoftDeserializerList()
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Book>>(_booksAsText, _newtonOptions);
    }
    
    [BenchmarkCategory("List"), Benchmark(Baseline = true)]
    public List<Book> SystemTextJsonDeserializerStringList()
    {
        return System.Text.Json.JsonSerializer.Deserialize<List<Book>>(_booksAsText, _systemOptions);
    }
    
    [BenchmarkCategory("List"), Benchmark]
    public List<Book> GeneratedDeserializerStringList()
    {
        return System.Text.Json.JsonSerializer.Deserialize(_booksAsText, JsonContext.Default.ListBook);
    }
    
    [BenchmarkCategory("Single"), Benchmark]
    public Book NewtonSoftDeserializer()
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Book>(_bookAsText, _newtonOptions);
    }
    
    [BenchmarkCategory("Single"), Benchmark(Baseline = true)]
    public Book SystemTextJsonDeserializerString()
    {
        return System.Text.Json.JsonSerializer.Deserialize<Book>(_bookAsText, _systemOptions);
    }
    
    [BenchmarkCategory("Single"), Benchmark]
    public Book GeneratedDeserializerString()
    {
        return System.Text.Json.JsonSerializer.Deserialize(_bookAsText, JsonContext.Default.Book);
    }
}