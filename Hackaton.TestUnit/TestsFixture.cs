using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CollectionDefinition(nameof(TestsFixtureCollection))]
public class TestsFixtureCollection : ICollectionFixture<TestsFixture>
{
}

public class TestsFixture : IDisposable
{
    private ApplicationDbContext? _applicationDbContext { get; set; }
    
    public ApplicationDbContext GetDbContext()
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("File")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _applicationDbContext = new ApplicationDbContext(contextOptions);

        _applicationDbContext.Database.EnsureDeleted();
        _applicationDbContext.Database.EnsureCreated();

        return _applicationDbContext;
    }

    private File GetFileWithAuthor()
    {
        var test = new Faker<File>()
            .CustomInstantiator(f =>
                new File(
                    name: f.Name.FirstName(),
                    sizeInBytes: f.Random.Long(1000, 1000000),
                    url: f.Internet.Url(),
                    contentType: f.Internet.ContentType(),
                    processStatusId: f.Random.Uuid().ToString()
                )
            );

        var author = test.Generate();

        var testNews = new Faker<File>()
            .CustomInstantiator(f =>
                new File(
                    name: f.Name.FirstName(),
                    sizeInBytes: f.Random.Long(1000, 1000000),
                    url: f.Internet.Url(),
                    contentType: f.Internet.ContentType(),
                    processStatusId: f.Random.Uuid().ToString()
                )
            );

        return testNews.Generate();
    }

    public Guid AddFileToDbContext()
    {
        var file = GetFileWithAuthor();

        _applicationDbContext?.File.Add(file);
        _applicationDbContext?.SaveChanges();

        return file.Id;
    }

    public ApiResponse? GetApiResponseFromObjectResult(ObjectResult? objectResult)
    {
        return (ApiResponse?)objectResult?.Value;
    }

    public T? ConvertDataFromObjectResult<T>(ObjectResult? objectResult)
    {
        return (T?)GetApiResponseFromObjectResult(objectResult)?.Data;
    }

    public void Dispose()
    {
        _applicationDbContext?.Dispose();
    }
}