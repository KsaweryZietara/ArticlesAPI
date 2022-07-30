using System.ComponentModel.DataAnnotations;

namespace ArticlesAPI.Api.Dtos{

    public class DisplayArticleDto{
        public string? Author { get; set; }

        public string? Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string? Content { get; set; }

        public int Likes { get; set; }
    }
}