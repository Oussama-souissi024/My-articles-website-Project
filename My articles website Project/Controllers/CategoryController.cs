using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;

namespace My_articles_website_Project.Controllers
{
    [Authorize("Admin")]
    public class CategoryController : Controller
    {
        private readonly IDataHelper<Category> dataHelper;

        public CategoryController(IDataHelper<Category> dataHelper)
        {
            this.dataHelper = dataHelper;
        }
        // GET: CategoryController
        public ActionResult Index(int? id)
        {
            if(id == 0 || id == null)
            {
				return View(dataHelper.GetAllData().Take(5));
			}
            else
            {
                var data = dataHelper.GetAllData().Where(x => x.ID > id).Take(5);
                return View(data);
            }
            
        }

        public IActionResult Search (string SearchItem)
        {
            if (SearchItem != null)
                return View("Index", dataHelper.Search(SearchItem));
            else
                return View("Index", dataHelper.GetAllData());
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                var result = dataHelper.Add(category);
                return (result == 1) ? RedirectToAction(nameof(Index)) : View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                var result = dataHelper.Edit(id, category);
                return (result == 1) ? RedirectToAction(nameof(Index)) : View() ;
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id , Category category)
        {
            try
            {
                var result = dataHelper.Delete(id);
                return  (result == 1) ? RedirectToAction(nameof(Index)) : View();
            }
            catch
            {
                return View();
            }
        }
    }
}
