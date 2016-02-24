namespace StackFaceSystem.Web.Areas.Moderator.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.ManageCategories;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
    public class ManageCategoriesController : BaseController
    {
        private const int CategoriesPerPage = 10;
        private readonly ICategoriesService categories;
        private readonly IPostsService posts;
        private readonly IAnswersService answers;
        private readonly ICommentsService comments;

        public ManageCategoriesController(IPostsService posts, IAnswersService answers, ICommentsService comments, ICategoriesService categories)
        {
            this.posts = posts;
            this.answers = answers;
            this.comments = comments;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            int page = id;
            var categoriesCount = this.categories.GetAllCategoriesCount();
            var sortType = "Id";
            var sortDirection = "ascending";
            var totalPages = (int)Math.Ceiling(categoriesCount / (decimal)CategoriesPerPage);

            var categories = this.categories.GetCategoriesByPageAndSort(sortType, sortDirection, page, CategoriesPerPage).To<CategoryViewModel>().ToList();

            var viewModel = new PagableSortableCategoriesViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                SortType = sortType,
                SortDirection = sortDirection,
                Categories = categories
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PagableSortableCategoriesViewModel model)
        {
            int page = model.CurrentPage;
            var categoriesCount = this.categories.GetAllCategoriesCount();
            var sortType = model.SortType;
            var sortDirection = model.SortDirection;
            var totalPages = (int)Math.Ceiling(categoriesCount / (decimal)CategoriesPerPage);

            var categories = this.categories.GetCategoriesByPageAndSort(sortType, sortDirection, page, CategoriesPerPage).To<CategoryViewModel>().ToList();

            var viewModel = new PagableSortableCategoriesViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                SortType = sortType,
                SortDirection = sortDirection,
                Categories = categories
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return this.PartialView("_CreateCategoryPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(InputCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Something is wrong, please try again later";
                return this.View(model);
            }

            var category = this.Mapper.Map<Category>(model);

            this.categories.CreateCategory(category);
            this.TempData["Notification"] = "You successfully add category";

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id, string name)
        {
            var categoryToEdit = new EditCategoryViewModel
            {
                Id = id,
                Name = name
            };

            return this.PartialView("_EditCategoryPartial", categoryToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(EditCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Something is wrong, please try again later";
                return this.View(model);
            }

            this.categories.UpdateCategory(model.Id, model.Name);
            this.TempData["Notification"] = "You successfully update category";

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int subjectId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            if (this.Request.IsAjaxRequest())
            {
                // Get category
                var category = this.categories.GetById(subjectId);

                var postsCount = this.posts.GetPostsCountByCategory(category.Name);

                var posts = this.posts.GetPostByCategory(category.Name, 1, postsCount).ToList();

                foreach (var post in posts)
                {
                    // get answers on this post
                    var numberOfAnswersToDelete = this.answers.GetAnswerCountPerPost(post.Id);
                    var answers = this.answers.GetAnswerOnPost(post.Id, 1, numberOfAnswersToDelete).ToList();

                    // delete comments on those answers
                    foreach (var answer in answers)
                    {
                        this.comments.DeleteCommentByAnswerId(answer.Id);
                    }

                    // delete answers on this post
                    this.answers.DeleteAnswerByPostId(post.Id);

                    // delete post
                    this.posts.DeletePost(post);
                }

                this.categories.DeleteCategory(category);

                return this.Json(new { notification = "You successfully delete category." });
            }

            return this.View("Index");
        }
    }
}