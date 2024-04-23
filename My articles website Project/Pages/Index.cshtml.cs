using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;
namespace My_articles_website_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<MyArticlesWebsiteProject.Core.Category> dataHelperCategory;
        private readonly IDataHelper<AuthorPost> dataHelperAuthorPost;
		public readonly int NoOfItem;

		public IndexModel(
            ILogger<IndexModel> logger,
            IDataHelper<MyArticlesWebsiteProject.Core.Category> dataHelperCategory,
            IDataHelper<MyArticlesWebsiteProject.Core.AuthorPost> dataHelperAuthorPost
            )
        {
            _logger = logger;
            this.dataHelperCategory = dataHelperCategory;
            this.dataHelperAuthorPost = dataHelperAuthorPost;

			NoOfItem = 6;
			listOfCategory = new List<MyArticlesWebsiteProject.Core.Category>();
			listOfPost = new  List<MyArticlesWebsiteProject.Core.AuthorPost>();
	}

        public List<MyArticlesWebsiteProject.Core.Category>  listOfCategory { get; set; }

		public List<MyArticlesWebsiteProject.Core.AuthorPost> listOfPost { get; set; }
		public void OnGet(string LoadState, string CategoryName, string SearchItem,int? id)
        {
			GetAllCategory();

			if (LoadState == null || LoadState == "All")
            {
				GetAllPost();
			}
            else if(LoadState == "ByCategory")
            {
                GetDataByCategoryName(CategoryName);

			}
            else if(LoadState == "Search")
            {
				Search(SearchItem);
			}
            else if(LoadState == "Next")
            {
                GetNextItem(id);
            }
            else if(LoadState == "Prev")
            {
                GetPreviousItem(id);
            }
			

		}

        private void GetAllCategory()
        {
            listOfCategory = dataHelperCategory.GetAllData().ToList();
        }

        private void GetAllPost()
        {
            listOfPost = dataHelperAuthorPost.GetAllData().Take(NoOfItem).ToList();
        }

		private void GetDataByCategoryName(string CategoryName)
		{
			listOfPost = dataHelperAuthorPost.GetAllData().Where(x => x.PostCategory == CategoryName).Take(NoOfItem).ToList();
		}

        private void Search(string SearchItem)
        {
            listOfPost = dataHelperAuthorPost.Search(SearchItem).Take(NoOfItem).ToList();
        }

        private void GetNextItem(int? id)
        {
            listOfPost = dataHelperAuthorPost.GetAllData().Where(x => x.ID > id).Take(NoOfItem).ToList();
        }

		private void GetPreviousItem(int? id)
		{
			listOfPost = dataHelperAuthorPost.GetAllData().Where(x => x.ID < id).Take(NoOfItem).ToList();
		}
	}
}
