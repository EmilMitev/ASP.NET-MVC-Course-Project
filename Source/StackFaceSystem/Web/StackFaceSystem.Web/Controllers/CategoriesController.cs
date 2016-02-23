namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Categories;
    using ViewModels.Posts;

    public class CategoriesController : BaseController
    {
        private const int ItemsPerPage = 5; // TODO: change it to 10
        private readonly ICategoriesService categories;
        private readonly IPostsService posts;

        public CategoriesController(ICategoriesService categories, IPostsService posts)
        {
            this.categories = categories;
            this.posts = posts;
        }

        [HttpGet]
        public ActionResult Index(string id, int page = 1)
        {
            var categoryFromDb = this.categories.GetCategory(id);
            var category = this.Mapper.Map<CategoryViewModel>(categoryFromDb);

            var posts = this.posts.GetPostByCategory(id, page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = this.posts.GetPostsNumberByCategory(id);
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            var viewModel = new PagablePostsOnCategoryViewModel
            {
                Category = category,
                CurrentPage = page,
                TotalPages = totalPages,
                Posts = posts
            };

            return this.View(viewModel);
        }
    }
}