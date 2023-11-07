using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using Xunit;

namespace MiniBlogTest
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public TestBase(CustomWebApplicationFactory<Startup> factory)
        {
            this.Factory = factory;
        }

        protected CustomWebApplicationFactory<Startup> Factory { get; }

        protected HttpClient GetClient(ArticleStore articleStore = null, UserStore userStore = null, IArticleRepository articleRepository = null, IUserRepository userRepository = null)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services.AddSingleton<ArticleStore>(provider =>
                        {
                            return articleStore;
                        });
                        services.AddSingleton<UserStore>(provider =>
                        {
                            return userStore;
                        });
                        services.AddScoped<ArticleService>();
                        services.AddScoped<IArticleRepository>(provider =>
                        {
                            return articleRepository;
                        });
                        services.AddScoped<IUserRepository>(provider =>
                        {
                            return userRepository;
                        });
                    });
            }).CreateDefaultClient();
        }

        protected HttpClient GetClient2(Mock<IArticleRepository> articleRepository, Mock<IUserRepository> userRepository)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services.AddSingleton(articleRepository.Object);
                        services.AddSingleton(userRepository.Object);
                        services.AddScoped<ArticleService>();
                        services.AddScoped<IArticleRepository>();
                        services.AddScoped<IUserRepository>();
                    });
            }).CreateClient();
        }
    }
}
