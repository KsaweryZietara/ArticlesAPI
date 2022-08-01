using System.Text;
using System.Text.Json;
using ArticlesAPI.Api.Data;
using ArticlesAPI.Api.Dtos;
using ArticlesAPI.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ArticlesAPI.Api.Controllers{
    
    [Route("api/[controller]")]
    [Controller]
    public class ArticlesController : ControllerBase{
        private readonly IAppRepo _repo;

        private readonly IMapper _mapper;

        private readonly IDistributedCache _cache;

        public ArticlesController(IAppRepo repo, IMapper mapper, IDistributedCache cache){
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        //POST api/articles/
        [HttpPost]
        public async Task<ActionResult> CreateArticleAsync([FromBody]CreateArticleDto createArticleDto){
            var article = _mapper.Map<ArticleModel>(createArticleDto);
            article.PublicationDate = DateTime.Now;
            article.Likes = 0;

            await _repo.CreateArticleAsync(article);
            await SaveToCache(article);
            
            return Ok("Article has been created.");
        }

        //GET api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticleByIdAsync([FromRoute]int id){
            ArticleModel? article = new ArticleModel();
            
            var cacheArticle = await _cache.GetAsync(id.ToString());
            
            if(cacheArticle != null){
                var cacheArticleString = Encoding.UTF8.GetString(cacheArticle);
                article = JsonSerializer.Deserialize<ArticleModel>(cacheArticleString);
            }
            else{
                article = _repo.GetArticleById(id);

                if(article == null){
                    return NotFound();
                }

                await SaveToCache(article);
            }

            return Ok(_mapper.Map<DisplayArticleDto>(article));
        }

        //GET api/articles/bypopularity/{amount}
        [HttpGet("bypopularity/{amount}")]
        public ActionResult GetMostPopular([FromRoute]int amount){
            IEnumerable<ArticleModel> articles = _repo.GetMostPopular(amount);

            if(articles.Count() == 0){
                return NotFound();
            } 

            return Ok(_mapper.Map<IEnumerable<DisplayArticleDto>>(articles));
        }

        //GET api/articles/bykeyword/{keyword}
        [HttpGet("bykeyword/{keyword}")]
        public ActionResult GetArticleByKeyWord([FromRoute]string keyWord){
            IEnumerable<ArticleModel> articles = _repo.GetArticleByKeyWord(keyWord);

            if(articles.Count() == 0){
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<DisplayArticleDto>>(articles));
        }

        //PUT api/articles/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> GiveLikeAsync([FromRoute]int id){
            var article = await _repo.GiveLikeAsync(id);
            
            if(article == null){
                return NotFound();
            }
            
            return NoContent();
        }

        private async Task SaveToCache(ArticleModel article){
            string articleString = JsonSerializer.Serialize(article);
            var articleToCache = Encoding.UTF8.GetBytes(articleString);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(60))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            await _cache.SetAsync(article.Id.ToString(), articleToCache, options);
        }
    }
}