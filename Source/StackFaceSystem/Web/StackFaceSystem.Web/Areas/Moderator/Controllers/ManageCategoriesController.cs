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
        private readonly ICategoriesService m_Categories;
        private readonly IPostsService m_Posts;
        private readonly IAnswersService m_Answers;
        private readonly ICommentsService m_Comments;

        public ManageCategoriesController(IPostsService posts, IAnswersService answers, ICommentsService comments, ICategoriesService categories)
        {
            m_Posts = posts;
            m_Answers = answers;
            m_Comments = comments;
            m_Categories = categories;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            int page = id;
            var categoriesCount = m_Categories.GetAllCategoriesCount();
            var sortType = "Id";
            var sortDirection = "ascending";
            var totalPages = (int)Math.Ceiling(categoriesCount / (decimal)CategoriesPerPage);

            var categories = m_Categories.GetCategoriesByPageAndSort(sortType, sortDirection, page, CategoriesPerPage).To<CategoryViewModel>().ToList();

            var viewModel = new PagableSortableCategoriesViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                SortType = sortType,
                SortDirection = sortDirection,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PagableSortableCategoriesViewModel model)
        {
            int page = model.CurrentPage;
            var categoriesCount = m_Categories.GetAllCategoriesCount();
            var sortType = model.SortType;
            var sortDirection = model.SortDirection;
            var totalPages = (int)Math.Ceiling(categoriesCount / (decimal)CategoriesPerPage);

            var categories = m_Categories.GetCategoriesByPageAndSort(sortType, sortDirection, page, CategoriesPerPage).To<CategoryViewModel>().ToList();

            var viewModel = new PagableSortableCategoriesViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                SortType = sortType,
                SortDirection = sortDirection,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return PartialView("_CreateCategoryPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(InputCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Something is wrong, please try again later";
                return View(model);
            }

            var category = Mapper.Map<Category>(model);

            m_Categories.CreateCategory(category);
            TempData["Notification"] = "You successfully add category";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id, string name)
        {
            var categoryToEdit = new EditCategoryViewModel
            {
                Id = id,
                Name = name
            };

            return PartialView("_EditCategoryPartial", categoryToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Something is wrong, please try again later";
                return View(model);
            }

            m_Categories.UpdateCategory(model.Id, model.Name);
            TempData["Notification"] = "You successfully update category";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int subjectId)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (Request.IsAjaxRequest())
            {
                // Get category
                var category = m_Categories.GetById(subjectId);

                var postsCount = m_Posts.GetPostsCountByCategory(category.Name);

                var posts = m_Posts.GetPostByCategory(category.Name, 1, postsCount).ToList();

                foreach (var post in posts)
                {
                    // get answers on  post
                    var numberOfAnswersToDelete = m_Answers.GetAnswerCountPerPost(post.Id);
                    var answers = m_Answers.GetAnswerOnPost(post.Id, 1, numberOfAnswersToDelete).ToList();

                    // delete comments on those answers
                    foreach (var answer in answers)
                    {
                        m_Comments.DeleteCommentByAnswerId(answer.Id);
                    }

                    // delete answers on  post
                    m_Answers.DeleteAnswerByPostId(post.Id);

                    // delete post
                    m_Posts.DeletePost(post);
                }

                m_Categories.DeleteCategory(category);

                return Json(new { notification = "You successfully delete category." });
            }

            return View("Index");
        }
    }
}