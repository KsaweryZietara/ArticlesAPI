using System.ComponentModel.DataAnnotations;

namespace ArticlesAPI.Api.Models{

    public class ArticleModel{
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public int Likes { get; set; }
    }
}