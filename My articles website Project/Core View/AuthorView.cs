using System.ComponentModel.DataAnnotations;

namespace My_articles_website_Project.Core_View
{
    public class AuthorView
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }

        public IFormFile? ProfileImageUrl { get; set; }

        [MinLength(10, ErrorMessage = "Minimum Length of characters is 10")]
        public string? Bio { get; set; }
        public string? Facebook { get; set; }
    }
}
