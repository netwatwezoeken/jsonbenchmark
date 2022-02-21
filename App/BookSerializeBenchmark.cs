using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;

namespace App;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class BookSerializeBenchmark
{
    private List<Book> _books = new();
    private readonly Newtonsoft.Json.JsonSerializerSettings _newtonOptions = SerializationOptions.Newtonsoft;
    private readonly System.Text.Json.JsonSerializerOptions _systemOptions = SerializationOptions.SystemTextJson;

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
    }
    
    [BenchmarkCategory("List"), Benchmark]
    public string NewtonSoftSerializerList()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(_books, _newtonOptions);
    }
    
    [BenchmarkCategory("List"), Benchmark(Baseline = true)]
    public string SystemTextJsonSerializerStringList()
    {
        return System.Text.Json.JsonSerializer.Serialize(_books, _systemOptions);
    }
    
    [BenchmarkCategory("List"), Benchmark]
    public string GeneratedSerializerStringList()
    {
        return System.Text.Json.JsonSerializer.Serialize(_books, JsonContext.Default.ListBook);
    }
    
    [BenchmarkCategory("Single"), Benchmark]
    public string NewtonSoftSerializer()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(_books[0], _newtonOptions);
    }
    
    [BenchmarkCategory("Single"), Benchmark(Baseline = true)]
    public string SystemTextJsonSerializerString()
    {
        return System.Text.Json.JsonSerializer.Serialize(_books[0], _systemOptions);
    }
    
    [BenchmarkCategory("Single"), Benchmark]
    public string GeneratedSerializerString()
    {
        return System.Text.Json.JsonSerializer.Serialize(_books[0], JsonContext.Default.Book);
    }
}