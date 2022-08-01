using ArticlesAPI.Api.Models;

namespace ArticlesAPI.Api.Data{

    public interface IAppRepo{
        Task CreateArticleAsync(ArticleModel article);

        ArticleModel? GetArticleById(int id);

        IEnumerable<ArticleModel> GetMostPopular(int amount);

        IEnumerable<ArticleModel> GetArticleByKeyWord(string keyWord);

        Task<ArticleModel?> GiveLikeAsync(int id);
    }
}