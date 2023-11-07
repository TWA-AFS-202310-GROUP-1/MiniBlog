using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public async Task Should_create_article_when_invoke_CreateArticle_given_input_articleAsync()
    {
        var article = new Article("Jerry", "Let's code", "c#");
        var userStore = new UserStore();
        var articleService = new ArticleService(userStore, MockArticleRepo(article));
        var beforeCount = 2;

        var newArticle = await articleService.CreateArticle(article);

        Assert.Equal(beforeCount + 1, articleService.GetAll().Result.Count);
        Assert.Equal(article.Title, newArticle.Title);
        Assert.Equal(article.Content, newArticle.Content);
        Assert.Equal(article.UserName, newArticle.UserName);
    }

    [Fact]
    public async Task Should_return_all_articles_when_invoke_GetAll()
    {
        var userStore = new UserStore();
        var articles = new List<Article>
        {
            new Article(null, "Happy new year", "Happy 2021 new year"),
            new Article(null, "Happy Halloween", "Halloween is coming"),
        };
        var article = new Article("Jerry", "Let's code", "c#");
        var articleService = new ArticleService(userStore, MockArticleRepo(article));
        var result = await articleService.GetAll();

        Assert.Equal(3, result.Count);
        Assert.Equal(articles[0].Title, result[0].Title);
        Assert.Equal(articles[0].Content, result[0].Content);
        Assert.Equal(articles[0].UserName, result[0].UserName);
        Assert.Equal(articles[1].Title, result[1].Title);
        Assert.Equal(articles[1].Content, result[1].Content);
        Assert.Equal(articles[1].UserName, result[1].UserName);
    }

    [Fact]
    public async Task Should_return_article_when_invoke_GetById_given_id()
    {
        var userStore = new UserStore();
        var article = new Article("Jerry", "Let's code", "c#");
        var articleService = new ArticleService(userStore, MockArticleRepo(article));
        string id = articleService.CreateArticle(article).Result.Id;
        var result = articleService.GetById(id);
        Assert.Equal(id, result.Result.Id);
    }

    private IArticleRepository MockArticleRepo(Article article)
    {
        var mock = new Mock<IArticleRepository>();
        mock.Setup(repository => repository.CreateArticle(article)).Returns(Task.FromResult(new Article
        {
            Id = "mockId",
            UserName = article.UserName,
            Title = article.Title,
            Content = article.Content,
        }));
        mock.Setup(repository => repository.GetArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
                article,
            }));
        mock.Setup(repository => repository.GetById(It.IsAny<string>())).Returns(Task.FromResult(new Article
        {
            Id = "mockId",
            UserName = article.UserName,
            Title = article.Title,
            Content = article.Content,
        }));
        return mock.Object;
    }
}
