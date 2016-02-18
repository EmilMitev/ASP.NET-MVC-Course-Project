namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels;
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
            var postsList = this.posts.GetPostsByPage(1, 5).To<PostsViewModel>().ToList();

            //var categories =
            //    this.Cache.Get(
            //        "categories",
            //        () => this.jokeCategories.GetAll().To<JokeCategoryViewModel>().ToList(),
            //        30 * 60);

            return this.View(postsList);
        }
    }
}