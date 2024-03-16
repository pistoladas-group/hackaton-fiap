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
            .UseInMemoryDatabase("TechNews")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _applicationDbContext = new ApplicationDbContext(contextOptions);

        _applicationDbContext.Database.EnsureDeleted();
        _applicationDbContext.Database.EnsureCreated();

        return _applicationDbContext;
    }

    private News GetFileWithAuthor()
    {
        var testAuthor = new Faker<Author>()
            .CustomInstantiator(f =>
                new Author(
                    name: f.Name.FirstName(),
                    email: f.Internet.Email(),
                    imageSource: f.Image.PicsumUrl()
                )
            );

        var author = testAuthor.Generate();

        var testNews = new Faker<News>()
            .CustomInstantiator(f =>
                new News(
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
        var news = GetFileWithAuthor();

        _applicationDbContext?.News.Add(news);
        _applicationDbContext?.SaveChanges();

        return news.Id;
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