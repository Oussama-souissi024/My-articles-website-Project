using MyArticlesWebsiteProject.Core;
using System.ComponentModel.DataAnnotations;

namespace My_articles_website_Project.Core_View
{
    public class AuthorPostView
    {
   
        public int ID { get; set; }
       
        public string UserID { get; set; }
  
        public string UserName { get; set; }
       
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PostCategory { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string PostDescription { get; set; }

        [Required(ErrorMessage = "You need to upload your post image")]
        public IFormFile PostImageUrl { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime AddedDate { get; set; }

        //Navigation Area

        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
