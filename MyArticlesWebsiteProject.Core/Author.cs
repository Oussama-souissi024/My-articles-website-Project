using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProject.Core
{
    public class Author
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        
        public string? ProfileImageUrl { get; set; }

        [MinLength(10,ErrorMessage = "Minimum Length of characters is 10")]
        public string? Bio { get; set; }
        public string? Facebook { get; set; }

		//Navigation 
		public virtual List<AuthorPost> AuthorPosts { get; set; }
	}
}
