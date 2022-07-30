using System.ComponentModel.DataAnnotations;

namespace ArticlesAPI.Api.Dtos{

    public class CreateArticleDto{
        [Required]
        [MaxLength(50)]
        public string? Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }
        
        [Required]
        public string? Content { get; set; }
    }
}