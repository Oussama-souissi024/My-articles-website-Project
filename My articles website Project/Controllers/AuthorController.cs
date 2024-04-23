using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace My_articles_website_Project.Controllers
{
    public class AuthorController : Controller
    {
		private readonly IDataHelper<Author> dataHelper;
        private readonly Code.FilesHelper fielsHelper;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;

        public AuthorController(IDataHelper<Author> dataHelper, IWebHostEnvironment webHost, IAuthorizationService authorizationService) 
        {
			this.dataHelper = dataHelper;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            fielsHelper = new Code.FilesHelper(this.webHost);

        }
		// GET: AuthorController
		[Authorize("Admin")]
		public ActionResult Index(int? id)
		{
			if (id == 0 || id == null)
			{
				return View(dataHelper.GetAllData().Take(5));
			}
			else
			{
				var data = dataHelper.GetAllData().Where(x => x.ID > id).Take(5);
				return View(data);
			}
		}

		[Authorize("Admin")]
		public IActionResult Search(string SearchItem)
		{
			if (SearchItem != null)
				return View("Index", dataHelper.Search(SearchItem));
			else
				return View("Index", dataHelper.GetAllData());
		}

		// GET: AuthorController/Edit/5
		[Authorize]
		public ActionResult Edit(int id)
        {
            var author = dataHelper.Find(id);
            if (author != null)
            {
                Core_View.AuthorView authorView = new Core_View.AuthorView
                {
                    ID = author.ID,
                    UserID = author.UserID,
                    FullName = author.FullName,
                    UserName = author.UserName,
                    Bio = author.Bio,
                    Facebook = author.Facebook,
                };
                return View(authorView);
            }
            else
            {
                // Handle case where author with the specified ID does not exist
                return NotFound();
            }
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult Edit(int id, Core_View.AuthorView collection)
        {
            try
            {
                var author = new Author
                {
                    ID = collection.ID,
                    UserID = collection.UserID,
                    FullName = collection.FullName,
                    UserName = collection.UserName,
                    Bio = collection.Bio,
                    Facebook = collection.Facebook,
                    ProfileImageUrl = fielsHelper.UploadFile(collection.ProfileImageUrl, "images")
                };
                dataHelper.Edit(id,author);

                var result = authorizationService.AuthorizeAsync(User, "Admin");
                if(result.Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Redirect("/AdminIndex");
                }

            }
            catch(Exception ex)
            {
                return View();
            }
        }

		// GET: AuthorController/Delete/5
		[Authorize("Admin")]
		public ActionResult Delete(int id)
        {
            var author = dataHelper.Find(id);
            if (author != null)
            {
                return View(author);
            }
            else
            {
                // Handle case where author with the specified ID does not exist
                return NotFound();
            }
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize("Admin")]
		public ActionResult Delete(int id, Author collection)
        {
            try
            {
                dataHelper.Delete(id);
                string FilePath = "~/images/" + collection.ProfileImageUrl;
                if(System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
