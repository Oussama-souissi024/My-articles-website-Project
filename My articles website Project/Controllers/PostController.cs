using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;
using System.Security.Claims;

namespace My_articles_website_Project.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly IDataHelper<Author> dataHelperForAuthor;
        private readonly IDataHelper<Category> dataHelperForCategory;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly Code.FilesHelper filesHelper;
        private Task<AuthorizationResult> result;
        private string UserId;

        public PostController(
            IDataHelper<AuthorPost> dataHelper,
            IDataHelper<Author> dataHelperForAuthor,
            IDataHelper<Category> dataHelperForCategory,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForCategory = dataHelperForCategory;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            filesHelper = new Code.FilesHelper(this.webHost);
        }

        // GET: PostController
        public ActionResult Index(int? id)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                // Admin
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
            else
            {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(5));
                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.ID > id).Take(5);
                    return View(data);
                }
            }


        }

        public ActionResult Search(string SearchItem)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetAllData());
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem));
                }
            }
            else
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUser(UserId));
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem).Where(x => x.UserID == UserId).ToList());
                }
            }

        }
        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Core_View.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorID = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.ID).First(),
                    Category = collection.Category,
                    CategoryID = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.ID).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserID = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images")
                };
                dataHelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorpost = dataHelper.Find(id);
            Core_View.AuthorPostView authorPostView = new Core_View.AuthorPostView
            {
                AddedDate = authorpost.AddedDate,
                Author = authorpost.Author,
                AuthorID = authorpost.AuthorID,
                Category = authorpost.Category,
                CategoryID = authorpost.CategoryID,
                FullName = authorpost.FullName,
                PostCategory = authorpost.PostCategory,
                PostDescription = authorpost.PostDescription,
                PostTitle = authorpost.PostTitle,
                UserID = authorpost.UserID,
                UserName = authorpost.UserName,
                ID = authorpost.ID
            };
            return View(authorPostView);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Core_View.AuthorPostView collection)
        {
            try
            {
                SetUser();

                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorID = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.ID).First(),
                    Category = collection.Category,
                    CategoryID = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.ID).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserID = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserID == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images"),
                    ID = collection.ID
                };
                dataHelper.Edit(id, Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorPost collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.PostImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}