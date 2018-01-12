namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Answers;
    using ViewModels.Categories;
    using ViewModels.Posts;

    public class PostsController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IPostsService m_Posts;
        private readonly IAnswersService m_Answers;
        private readonly ICommentsService m_Comments;
        private readonly ICategoriesService m_Categories;
        private readonly ITagsService m_Tags;

        public PostsController(IPostsService posts, IAnswersService answers, ICategoriesService categories, ITagsService tags, ICommentsService comments)
        {
            m_Posts = posts;
            m_Answers = answers;
            m_Categories = categories;
            m_Tags = tags;
            m_Comments = comments;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            var page = id;
            var posts = m_Posts.GetPostsByPageAndSort("Date", "descending", string.Empty, page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = m_Posts.GetPostsCount();
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            var viewModel = new PagablePostsViewModel
            {
                SortField = "Date",
                SortDirection = "descending",
                CurrentPage = page,
                TotalPages = totalPages,
                Posts = posts
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PagablePostsViewModel model)
        {
            var page = model.CurrentPage;
            var sortType = model.SortField;
            var sortDirection = model.SortDirection;
            var searchValue = model.Search;

            var posts = m_Posts.GetPostsByPageAndSort(sortType, sortDirection, searchValue, page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = 0;
            var totalPages = 0;

            if (model.Search == string.Empty || model.Search == null)
            {
                postsNumber = m_Posts.GetPostsCount();
            }
            else
            {
                postsNumber = posts.Count();
            }

            totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            model.TotalPages = totalPages;
            model.Posts = posts;

            var viewModel = model;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(string id, int page = 1)
        {
            var post = m_Posts.GetById(id);
            var answersNumbers = m_Answers.GetAnswerCountPerPost(post.Id);
            var totalPages = (int)Math.Ceiling(answersNumbers / (decimal)ItemsPerPage);

            var answers = m_Answers
                                    .GetAnswerOnPost(post.Id, page, ItemsPerPage)
                                    .To<AnswersViewModel>()
                                    .ToList();

            var viewModel = new DetailsPostWithPagableAnswersViewModel
            {
                Post = Mapper.Map<DetailsPostViewModel>(post),
                CurrentPage = page,
                TotalPages = totalPages,
                Answers = answers
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            var categories = m_Categories.GetAllCategories().To<CategoriesViewModel>().ToList();

            var inputModel = new InputPostViewModel
            {
                Categories = categories
            };

            return View(inputModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(InputPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = m_Categories.GetAllCategories().To<CategoriesViewModel>().ToList();

                model.Categories = categories;

                return View(model);
            }

            var tags = m_Tags.CheckExist(model.Tags).ToList();

            var userId = User.Identity.GetUserId();

            var post = new Post
            {
                CategoryId = model.CategoryId,
                Content = model.Content,
                Tags = tags,
                Title = model.Title,
                UserId = userId
            };

            m_Posts.CreatePost(post);
            TempData["Notification"] = "You successfully add your post.";
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(string postId)
        {
            var postFromDb = m_Posts.GetById(postId);
            var post = Mapper.Map<EditPostViewModel>(postFromDb);
            return PartialView("_EditPostPartial", post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(EditPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            m_Posts.UpdatePost(model.EncodedId, model.Title, model.Content);

            TempData["Notification"] = "You successfully update your post.";
            return Redirect($"/Posts/Details/{model.EncodedId}");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(string postId)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return Redirect($"/Posts/Details/{postId}");
            }

            if (Request.IsAjaxRequest())
            {
                // get post
                var post = m_Posts.GetById(postId);

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

                return Json(new { notification = "You successfully delete post." });
            }

            // Don't work in ajax!!!
            return RedirectToAction("Index");
        }
    }
}