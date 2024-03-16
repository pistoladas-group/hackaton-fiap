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
        var testAuthor = new Faker<File>()
            .CustomInstantiator(f =>
                new File(
                    name: f.Name.FirstName(),
                    email: f.Internet.Email(),
                    imageSource: f.Image.PicsumUrl()
                )
            );

        var author = testAuthor.Generate();

        var testNews = new Faker<File>()
            .CustomInstantiator(f =>
                new File(
                    title: string.Join(" ", f.Lorem.Words(f.Random.Number(5, 10))),
                    description: f.Lorem.Paragraphs(),
                    publishDate: f.Date.Recent(),
                    author: author,
                    imageSource: f.Image.PicsumUrl()
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