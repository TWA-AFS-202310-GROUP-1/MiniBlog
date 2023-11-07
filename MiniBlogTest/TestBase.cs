using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
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

        protected HttpClient GetClient(IArticleRepository articleRepository = null, IUserRepository userRepository = null)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
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
    }
}
