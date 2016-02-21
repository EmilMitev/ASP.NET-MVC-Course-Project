namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels.Posts;

    public class PostsController : BaseController
    {
        private const int ItemsPerPage = 5;
        private const int CommentPerAnswer = 3;

        private readonly IPostsService posts;
        private readonly IAnswersService answers;
        private readonly ICategoriesService categories;
        private readonly ITagsService tags;

        public PostsController(IPostsService posts, IAnswersService answers, ICategoriesService categories, ITagsService tags)
        {
            this.posts = posts;
            this.answers = answers;
            this.categories = categories;
            this.tags = tags;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            var page = id;
            var posts = this.posts.GetPostsByPage(page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = this.posts.GetPostsNumber();
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            var viewModel = new PagablePostsViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Posts = posts
            };

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

        [HttpGet]
        public ActionResult Create()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InputPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.Cache.Get("categories", () => this.categories.GetAllCategories().To<CategoriesViewModel>().ToList(), 60 * 60);
                model.Categories = categories;

                return this.View(model);
            }

            // TODO:implement tags!!!!!
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

            // this.posts.CreatePost(post);
            this.TempData["Notification"] = "You successfully add your post.";
            return this.RedirectToAction("Index");
        }
    }
}