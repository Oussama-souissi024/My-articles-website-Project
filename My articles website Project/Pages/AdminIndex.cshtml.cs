using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;
using System.Security.Claims;

namespace My_articles_website_Project.Pages
{
    [Authorize]
    public class AdminIndexModel : PageModel
    {
		private readonly IDataHelper<AuthorPost> dataHelper;

		public AdminIndexModel(IDataHelper<AuthorPost> dataHelper)
        {
			this.dataHelper = dataHelper;
		}

        public int AllPost { get; set; }
        public int PostLastMonth { get; set; }
        public int PostThisYear { get; set; }

        public void OnGet()
        {
            var dateM = DateTime.Now.AddMonths(-1);
            var dateY = DateTime.Now.AddYears(-1);
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AllPost = dataHelper.GetDataByUser(userID).Count;
			PostLastMonth = dataHelper.GetDataByUser(userID).Where(x => x.AddedDate >= dateM).Count();
			PostThisYear = dataHelper.GetDataByUser(userID).Where(x => x.AddedDate >= dateY).Count();
        }
    }
}
