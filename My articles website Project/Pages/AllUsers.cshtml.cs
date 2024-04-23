using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyArticlesWebsiteProjectData;
using MyArticlesWebsiteProject.Core;

namespace MyArticlesWebsiteProject.Pages
{
	public class AllUsersModel : PageModel
	{
		public readonly int NoOfItem;
		private readonly IDataHelper<Author> dataHelper;

		public AllUsersModel(
			IDataHelper<Author> dataHelper
			)
		{
			this.dataHelper = dataHelper;
			NoOfItem = 6;
			ListOfAuthors = new List<Author>();
			this.dataHelper = dataHelper;
		}

		public List<Author> ListOfAuthors { get; set; }


		public void OnGet(string LoadState, string search, int id)
		{

			if (LoadState == null || LoadState == "All")
			{
				GetALLAuthors();

			}
			else if (LoadState == "Search")
			{
				SearchData(search);
			}
			else if (LoadState == "Next")
			{
				GetNextData(id);
			}
			else if (LoadState == "Prev")
			{
				GetNextData(id - NoOfItem);
			}
		}

		private void GetALLAuthors()
		{
			ListOfAuthors = dataHelper.GetAllData().Take(NoOfItem).ToList();
		}
		private void SearchData(string SearchItem)
		{
			ListOfAuthors = dataHelper.Search(SearchItem);
		}

		private void GetNextData(int id)
		{
			ListOfAuthors = dataHelper.GetAllData().Where(x => x.ID > id).Take(NoOfItem).ToList();
		}
	}
}
