namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Posts;

    public class HomeController : BaseController
    {
        private readonly IPostsService posts;

        public HomeController(IPostsService posts)
        {
            this.posts = posts;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var postsList = this.Cache
                                   .Get(
                                       "homePosts",
                                       () => this.posts.GetNewestPost().To<PostsViewModel>().ToList(),
                                       15 * 60);

            return this.View(postsList);
        }
    }
}