using ArticlesAPI.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesAPI.Api.Data{

    public class AppDbContext : DbContext{
        public DbSet<ArticleModel> Articles => Set<ArticleModel>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
        }
    }
}