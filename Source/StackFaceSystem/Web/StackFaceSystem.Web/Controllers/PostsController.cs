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
        private const int CommentPerAnswer = 3;

        private readonly IPostsService posts;
        private readonly IAnswersService answers;
        private readonly ICommentsService comments;
        private readonly ICategoriesService categories;
        private readonly ITagsService tags;

        public PostsController(IPostsService posts, IAnswersService answers, ICategoriesService categories, ITagsService tags, ICommentsService comments)
        {
            this.posts = posts;
            this.answers = answers;
            this.categories = categories;
            this.tags = tags;
            this.comments = comments;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            var page = id;
            var posts = this.posts.GetPostsByPageAndSort("Date", "descending", page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = this.posts.GetPostsNumber();
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            var viewModel = new PagablePostsViewModel
            {
                SortField = "Date",
                SortDirection = "descending",
                CurrentPage = page,
                TotalPages = totalPages,
                Posts = posts
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PagablePostsViewModel model)
        {
            var page = model.CurrentPage;
            var sortType = model.SortField;
            var sortDirection = model.SortDirection;

            var posts = this.posts.GetPostsByPageAndSort(sortType, sortDirection, page, ItemsPerPage).To<PostsViewModel>().ToList();

            var postsNumber = this.posts.GetPostsNumber();
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            model.TotalPages = totalPages;
            model.Posts = posts;

            var viewModel = model;

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(string id, int page = 1)
        {
            var post = this.posts.GetById(id);
            var answersNumbers = this.answers.GetAnswerNumberPerPost(post.Id);
            var totalPages = (int)Math.Ceiling(answersNumbers / (decimal)ItemsPerPage);

            var answers = this.answers
                                    .GetAnswerOnPost(post.Id, page, ItemsPerPage)
                                    .To<AnswersViewModel>()
                                    .ToList();

            var viewModel = new DetailsPostWithPagableAnswersViewModel
            {
                Post = this.Mapper.Map<DetailsPostViewModel>(post),
                CurrentPage = page,
                TotalPages = totalPages,
                Answers = answers
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            var categories = this.Cache
                                  .Get(
                                      "categories",
                                      () => this.categories.GetAllCategories().To<CategoriesViewModel>().ToList(),
                                      60 * 60);

            var inputModel = new InputPostViewModel
            {
                Categories = categories
            };

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(InputPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.Cache.Get("categories", () => this.categories.GetAllCategories().To<CategoriesViewModel>().ToList(), 60 * 60);
                model.Categories = categories;

                return this.View(model);
            }

            var tags = this.tags.CheckExist(model.Tags).ToList();

            var userId = this.User.Identity.GetUserId();

            var post = new Post
            {
                CategoryId = model.CategoryId,
                Content = model.Content,
                Tags = tags,
                Title = model.Title,
                UserId = userId
            };

            this.posts.CreatePost(post);
            this.TempData["Notification"] = "You successfully add your post.";
            return this.RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(string postId)
        {
            var postFromDb = this.posts.GetById(postId);
            var post = this.Mapper.Map<EditPostViewModel>(postFromDb);
            return this.PartialView("_EditPost", post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(EditPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.posts.UpdatePost(model.EncodedId, model.Title, model.Content);

            this.TempData["Notification"] = "You successfully update your post.";
            return this.Redirect($"/Posts/Details/{model.EncodedId}");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(string postId)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return this.Redirect($"/Posts/Details/{postId}");
            }

            if (this.Request.IsAjaxRequest())
            {
                // get post
                var post = this.posts.GetById(postId);

                // get answers on this post
                var numberOfAnswersToDelete = this.answers.GetAnswerNumberPerPost(post.Id);
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

                return this.Json(new { notification = "You successfully delete post." });
            }

            // Don't work in ajax!!!
            return this.RedirectToAction("Index");
        }
    }
}