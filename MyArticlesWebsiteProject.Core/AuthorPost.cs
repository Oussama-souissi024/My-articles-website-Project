using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProject.Core
{
	public class AuthorPost
	{
		[Required]
		public int ID { get; set; }

		[Required]
		public string UserID { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
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
		public string PostImageUrl { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public DateTime AddedDate { get; set; }

		//Navigation Area

		public int AuthorID { get; set; }
		public Author Author { get; set; }

		public int CategoryID { get; set; }
		public Category Category { get; set; }

	}
}
