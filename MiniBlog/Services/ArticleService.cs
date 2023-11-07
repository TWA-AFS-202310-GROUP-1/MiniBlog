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
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        var userList = await userRepository.GetUsers();
        article.Id = string.Empty;
        Article addedArticle = new Article();

        if (article.UserName != null)
        {
            if (!userList.Exists(_ => article.UserName == _.Name))
            {
                await userRepository.CreateUser(new User(article.UserName));
            }

            addedArticle = await articleRepository.CreateArticle(article);
        }

        return addedArticle;
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetById(string id)
    {
        return await articleRepository.GetArticleById(id);
    }
}
