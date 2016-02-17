namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class PostsController : BaseController
    {
        // GET: Posts
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Details(int id = 1)
        {
            return this.View();
        }
    }
}