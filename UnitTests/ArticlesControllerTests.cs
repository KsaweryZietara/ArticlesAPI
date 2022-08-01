using ArticlesAPI.Api.Controllers;
using ArticlesAPI.Api.Data;
using ArticlesAPI.Api.Dtos;
using ArticlesAPI.Api.Models;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace ArticlesAPI.UnitTests{
    
    public class ArticlesControllerTests{
        private readonly Mock<IAppRepo> repositoryStub = new Mock<IAppRepo>();

        private readonly Mock<IMapper> mapperStub = new Mock<IMapper>();

        private readonly Mock<IDistributedCache> cacheStub = new Mock<IDistributedCache>();

        [Fact]
        public async Task CreateArticleAsync_ArticleIsValid_ReturnsOk(){
            //Arrange
            mapperStub.Setup(mapper => mapper.Map<ArticleModel>(It.IsAny<CreateArticleDto>()))
                        .Returns(new ArticleModel());

            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = await controller.CreateArticleAsync(new CreateArticleDto());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetMostPopular_NumberOfArticlesIsZero_ReturnsNotFound(){
            //Arrange
            repositoryStub.Setup(repo => repo.GetMostPopular(It.IsAny<int>()))
                            .Returns(new ArticleModel[0]);

            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = controller.GetMostPopular(new int());

            // Then
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetMostPopular_NumberOfArticlesIsGraterThanZero_ReturnsOk(){
            //Arrange
            repositoryStub.Setup(repo => repo.GetMostPopular(It.IsAny<int>()))
                            .Returns(new List<ArticleModel>(){
                                new ArticleModel(),
                                new ArticleModel()
                            });

            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = controller.GetMostPopular(new int());

            // Then
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetArticleByKeyWord_NumberOfArticlesIsZero_ReturnsNotFound(){
            //Arrange
            repositoryStub.Setup(repo => repo.GetArticleByKeyWord(It.IsAny<string>()))
                            .Returns(new ArticleModel[0]);

            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = controller.GetArticleByKeyWord("test");

            // Then
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetArticleByKeyWord_NumberOfArticlesIsGraterThanZero_ReturnsOk(){
            //Arrange
            repositoryStub.Setup(repo => repo.GetArticleByKeyWord(It.IsAny<string>()))
                            .Returns(new List<ArticleModel>(){
                                new ArticleModel(),
                                new ArticleModel()
                            });

            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = controller.GetArticleByKeyWord("test");

            // Then
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GiveLikeAsync_ArticleIsNull_ReturnsNotFound(){
            //Arrange
            repositoryStub.Setup(repo => repo.GiveLikeAsync(It.IsAny<int>()))
                            .ReturnsAsync((ArticleModel)null);
            
            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = await controller.GiveLikeAsync(new int());

            // Then
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GiveLikeAsync_ArticleIsValid_ReturnsNoContent(){
            //Arrange
            repositoryStub.Setup(repo => repo.GiveLikeAsync(It.IsAny<int>()))
                            .ReturnsAsync(new ArticleModel());
            
            var controller = new ArticlesController(repositoryStub.Object, mapperStub.Object, cacheStub.Object);

            //Act
            var result = await controller.GiveLikeAsync(new int());

            // Then
            result.Should().BeOfType<NoContentResult>();
        }
    }
}