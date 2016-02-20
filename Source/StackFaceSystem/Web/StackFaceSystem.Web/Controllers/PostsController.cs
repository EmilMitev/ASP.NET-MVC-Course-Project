namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Posts;

    public class PostsController : BaseController
    {
        private const int ItemsPerPage = 5;
        private const int CommentPerAnswer = 3;

        private readonly IPostsService posts;
        private readonly IAnswersService answers;

        public PostsController(IPostsService posts, IAnswersService answers)
        {
            this.posts = posts;
            this.answers = answers;
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
    }
}