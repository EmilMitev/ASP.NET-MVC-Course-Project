namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Posts;
    using ViewModels;
    public class PostsController : BaseController
    {
        private readonly IPostsService posts;

        public PostsController(IPostsService posts)
        {
            this.posts = posts;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int skip = 5)
        {
            var postList = this.posts.GetPostsByPage(page, skip).To<PostsViewModel>().ToList();

            return this.View(postList);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var post = this.posts.GetById(id);
            var viewModel = this.Mapper.Map<DetailsViewModel>(post);
            return this.View(viewModel);
        }
    }
}