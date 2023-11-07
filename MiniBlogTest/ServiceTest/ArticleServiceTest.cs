using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    private readonly Mock<IArticleRepository> mockArticleRepository;
    private readonly ArticleService articleService;
    private readonly UserStore userStore;

    [Fact]
    public async Task Should_create_article_and_register_user_correct_when_create_article_service_given_article()
    {
        // given
        var articleToCreate = new Article("test", "test", "test");
        mockArticleRepository.Setup(repo => repo.CreateArticle(It.IsAny<Article>()))
                             .Callback<Article>(a => a.Id = Guid.NewGuid())
                             .ReturnsAsync((Article a) => a);

        // when
        var createdArticle = await articleService.CreateArticle(articleToCreate);

        // then
        Assert.NotNull(createdArticle.Id);
        Assert.Equal("test", createdArticle.UserName);
        Assert.Equal("test", createdArticle.Title);
        Assert.Equal("test", createdArticle.Content);
        mockArticleRepository.Verify(repo => repo.CreateArticle(It.IsAny<Article>()), Times.Once);
    }

    [Fact]
    public async Task ShouldGetArticleCorrect_WhenGetById_GivenExistingArticleId()
    {
        // given
        var articleId = Guid.NewGuid();
        var expectedArticle = new Article("test", "test", "test") { Id = articleId };
        mockArticleRepository.Setup(repo => repo.GetById(articleId))
                             .ReturnsAsync(expectedArticle);

        // when
        var foundArticle = await articleService.GetById(articleId);
        // then
        Assert.Equal(expectedArticle, foundArticle);
        mockArticleRepository.Verify(repo => repo.GetById(articleId), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnNull_WhenGetById_GivenNonExistingArticleId()
    {
        // Arrange
        var nonExistingArticleId = Guid.NewGuid();
        mockArticleRepository.Setup(repo => repo.GetById(nonExistingArticleId))
                             .ReturnsAsync((Article)null);

        // Act
        var foundArticle = await articleService.GetById(nonExistingArticleId);

        // Assert
        Assert.Null(foundArticle);
        mockArticleRepository.Verify(repo => repo.GetById(nonExistingArticleId), Times.Once);
    }
}
