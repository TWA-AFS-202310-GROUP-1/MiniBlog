using Microsoft.AspNetCore.Identity;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using Snappier;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public void Should_return_article_when_invoke_GetById_given_Id()
    {
        string id = "123";
        var mockArticle = new Mock<IArticleRepository>();
        var mockUser = new Mock<IUserRepository>();
        mockArticle.Setup(repository => repository.GetArticleById(id)).Returns(Task.FromResult(new Article("user", "Happy new year", "Happy 2021 new year")));

        var articleService = new ArticleService(mockArticle.Object, mockUser.Object);
        var articleGet = articleService.GetById(id);

        Assert.Equal("Happy new year", articleGet.Result.Title);
    }

    [Fact]
    public async void Should_create_article_and_not_create_user_function_be_invoked_when_invoke_createArticle_given_user_name_existed()
    {
        var mockArticle = new Mock<IArticleRepository>();
        var mockUser = new Mock<IUserRepository>();
        mockUser.Setup(repository => repository.GetUsers()).Returns(Task.FromResult(new List<User>
            {
                new User("Tom", " "),
                new User("Peter", " "),
            }));
        mockUser.Setup(obj => obj.CreateUser(new User("Jerry", "email"))).CallBase();

        var newArticle = new Article("Jerry", "Let's code", "c#");
        newArticle.Id = string.Empty;

        mockArticle.Setup(repository => repository.CreateArticle(newArticle)).Returns(Task.FromResult(newArticle));
        mockUser.Setup(repository => repository.CreateUser(new User())).Returns(Task.FromResult(new User("Jerry", "email")));

        var articleService = new ArticleService(mockArticle.Object, mockUser.Object);
        await articleService.CreateArticle(newArticle);

        mockArticle.Verify(repository => repository.CreateArticle(newArticle), Times.Once);
        mockUser.Verify(repository => repository.CreateUser(new User()), Times.Never);
    }

    [Fact]
    public async void Should_create_article_and_create_user_function_be_invoked_when_invoke_createArticle_given_user_name_not_existed()
    {
        var mockArticle = new Mock<IArticleRepository>();
        var mockUser = new Mock<IUserRepository>();

        mockUser.Setup(repository => repository.GetUsers()).Returns(Task.FromResult(new List<User>
            {
                new User("Tom", " "),
                new User("Peter", " "),
            }));
        var newArticle = new Article("Jerry", "Let's code", "c#");
        newArticle.Id = string.Empty;

        var newUser = new User("Jerry");

        mockArticle.Setup(repository => repository.CreateArticle(newArticle)).Returns(Task.FromResult(new Article("Jerry", "Let's code", "c#")));
        mockUser.Setup(repository => repository.CreateUser(newUser)).Returns(Task.FromResult(newUser));

        var articleService = new ArticleService(mockArticle.Object, mockUser.Object);
        await articleService.CreateArticle(newArticle);

        mockArticle.Verify(repository => repository.CreateArticle(newArticle), Times.Once);
        mockUser.Verify(repository => repository.CreateUser(newUser), Times.AtMostOnce);
    }
}
