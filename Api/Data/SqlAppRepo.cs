using ArticlesAPI.Api.Models;

namespace ArticlesAPI.Api.Data{

    public class SqlAppRepo : IAppRepo{
        private readonly AppDbContext _context;

        public SqlAppRepo(AppDbContext context){
            _context = context;
        }

        public async Task CreateArticleAsync(ArticleModel article){
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public ArticleModel? GetArticleById(int id){
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);
            return article;
        }

        public IEnumerable<ArticleModel> GetMostPopular(int amount){
            var articles = _context.Articles.OrderByDescending(x => x.Likes);
            
            if(articles.Count() < amount){
                return articles;
            }

            return articles.Take(amount);
        }
        
        public IEnumerable<ArticleModel> GetArticleByKeyWord(string keyWord){
            var articles = _context.Articles.Where(x => x.Author.Contains(keyWord) ||
                                                            x.Title.Contains(keyWord) ||
                                                            x.Content.Contains(keyWord));

            return articles;
        }   

        public async Task GiveLikeAsync(int id){
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);

            if(article != null){
                article.Likes++;
                await _context.SaveChangesAsync();
            }
        }
    }
}