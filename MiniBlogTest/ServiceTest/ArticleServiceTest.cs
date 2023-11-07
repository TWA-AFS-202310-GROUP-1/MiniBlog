using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest : TestBase
{
    public ArticleServiceTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
    {
    }

    [Fact]
    public async void Should_get_all_articles()
    {
        // given
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>()));
        var client = GetClient(new ArticleStore(), null, mock.Object, null);

        //When
        var response = await client.GetAsync("/article");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var articles = JsonConvert.DeserializeObject<List<User>>(body);

        //Then
        Assert.Equal(0, articles.Count);
    }

    [Fact]
    public async void Create_article_and_user_if_user_not_exist()
    {
        // given
        //var article = new Article("Tom", "Happy new year", "Happy 2021 new year");
        var mock1 = new Mock<IArticleRepository>();
        mock1.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }));
        var mock2 = new Mock<IUserRepository>();
        mock2.Setup(repository => repository.GetUsers()).Returns(Task.FromResult(new List<User>()));
        var articleService = new ArticleService(mock1.Object, mock2.Object);
        var newArticle = new Article("Jerry", "Let's code", "c#");
        var addedArticle = articleService.CreateArticle(newArticle).Result;
        /*var client = GetClient(new ArticleStore(), null, mock1.Object, mock2.Object);
        var httpContent = JsonConvert.SerializeObject(article);
        StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);*/

        //When
        /*var articleResponse = await client.PostAsync("/article", content);
        var articleResponse1 = await client.GetAsync("/article");
        var body = await articleResponse1.Content.ReadAsStringAsync();
        articleResponse1.EnsureSuccessStatusCode();
        var articleBody = await articleResponse1.Content.ReadAsStringAsync();
        var articleGet = JsonConvert.DeserializeObject<Article>(articleBody);*/
        /*var userRespomse = await client.GetAsync("/user");
        userRespomse.EnsureSuccessStatusCode();
        var userBody = await userRespomse.Content.ReadAsStringAsync();
        var userGet = JsonConvert.DeserializeObject<User>(userBody);*/

        //Then
        Assert.Equal("Jerry", addedArticle.UserName);
        /*Assert.Equal("Tom", userGet.Name);*/
    }
}
