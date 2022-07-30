using ArticlesAPI.Api.Dtos;
using ArticlesAPI.Api.Models;
using AutoMapper;

namespace ArticlesAPI.Api.Profiles{

    public class ArticlesProfile : Profile{
        public ArticlesProfile(){
            CreateMap<CreateArticleDto, ArticleModel>();
            CreateMap<ArticleModel, DisplayArticleDto>();
        }
    }
}
