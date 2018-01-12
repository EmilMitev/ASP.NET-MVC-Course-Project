namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Categories;
    using ViewModels.Posts;

    [Authorize]
    public class CategoriesController : BaseController
    {
        private const int ItemsPerPage = 5; // TODO: change it to 10
        private readonly ICategoriesService m_Categories;
        private readonly IPostsService m_Posts;

        public CategoriesController(ICategoriesService categories, IPostsService posts)
        {
            m_Categories = categories;
            m_Posts = posts;
        }

        [HttpGet]
        public ActionResult GetCategoryPosts(string id, int page = 1)
        {
            var categoryFromDb = m_Categories.GetCategory(id);
            var category = Mapper.Map<CategoryViewModel>(categoryFromDb);

            var posts = m_Posts.GetPostByCategory(id, page, ItemsPerPage).To<PostsViewModel>().ToList();
            var postsNumber = m_Posts.GetPostsCountByCategory(id);
            var totalPages = (int)Math.Ceiling(postsNumber / (decimal)ItemsPerPage);

            var viewModel = new PagablePostsOnCategoryViewModel
            {
                Category = category,
                CurrentPage = page,
                TotalPages = totalPages,
                Posts = posts
            };

            return View(viewModel);
        }
    }
}