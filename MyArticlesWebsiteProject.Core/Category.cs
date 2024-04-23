using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProject.Core
{
    public class Category
    {
        [Required]
        [Display(Name="Category ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name="Category Name")]
        [MaxLength(50,ErrorMessage =("Maximum Length of characters is 50"))]
        [MinLength(2,ErrorMessage = "Minimum Length of characters is 2")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }

        //Navigation 
        public virtual List<AuthorPost> AuthorPosts { get; set; }

	}
}
