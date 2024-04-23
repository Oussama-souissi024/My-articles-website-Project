using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyArticlesWebsiteProjectData;
using MyArticlesWebsiteProject.Core;

namespace My_articles_website_Project.Pages
{
    public class ArticleModel : PageModel
    {
		private readonly IDataHelper<AuthorPost> dataHelperForPost;
        public AuthorPost post { get; set; }

		public ArticleModel(IDataHelper<AuthorPost> dataHelperForPost)
        {
			this.dataHelperForPost = dataHelperForPost;
            post = new AuthorPost();
		}


        public void OnGet()
        {
            var id = HttpContext.Request.RouteValues["id"];
			post = dataHelperForPost.Find(Convert.ToInt32(id));
		}

       
    }
}
