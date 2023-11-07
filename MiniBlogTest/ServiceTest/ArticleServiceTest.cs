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

    // [Fact]
    // public void Should_create_article_when_invoke_CreateArticle_given_input_article()
    // {
    //     // given
    //     var newArticle = new Article("Jerry", "Let's code", "c#");
    //     var articleStore = new ArticleStore();
    //     var articleCountBeforeAddNewOne = articleStore.Articles.Count;
    //     var userStore = new UserStore();
    //     var articleService = new ArticleService(articleStore, userStore);

    //     // when
    //     var addedArticle = articleService.CreateArticle(newArticle);

    //     // then
    //     Assert.Equal(articleCountBeforeAddNewOne + 1, articleStore.Articles.Count);
    //     Assert.Equal(newArticle.Title, addedArticle.Title);
    //     Assert.Equal(newArticle.Content, addedArticle.Content);
    //     Assert.Equal(newArticle.UserName, addedArticle.UserName);
    // }
}
