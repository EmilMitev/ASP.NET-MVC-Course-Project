namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Posts;

    public class HomeController : BaseController
    {
        private readonly IPostsService m_Posts;

        public HomeController(IPostsService posts)
        {
            m_Posts = posts;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var postsList = Cache.Get(
                                    "homePosts",
                                    () => m_Posts.GetNewestPost().To<PostsViewModel>().ToList(),
                                    15 * 60);

            return View(postsList);
        }
    }
}