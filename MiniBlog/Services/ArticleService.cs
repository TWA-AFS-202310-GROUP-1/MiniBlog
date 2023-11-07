using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService
{
    private readonly UserStore userStore = null!;
    private readonly IArticleRepository articleRepository = null!;
    private IArticleRepository object1;
    private IUserRepository object2;

    public ArticleService(UserStore userStore, IArticleRepository articleRepository)
    {
        this.userStore = userStore;
        this.articleRepository = articleRepository;
    }

    public ArticleService(IArticleRepository object1, IUserRepository object2)
    {
        this.object1 = object1;
        this.object2 = object2;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        if (article.UserName != null)
        {
            if (!userStore.Users.Exists(_ => article.UserName == _.Name))
            {
                userStore.Users.Add(new User(article.UserName));
            }

            await articleRepository.CreateArticle(article);
        }

        return await this.articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article> GetById(Guid id)
    {
        return await articleRepository.GetById(id);
    }
}
