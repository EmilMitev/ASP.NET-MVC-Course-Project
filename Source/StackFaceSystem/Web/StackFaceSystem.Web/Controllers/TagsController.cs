namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;

    [Authorize]
    public class TagsController : BaseController
    {
        private readonly ITagsService tags;

        public TagsController(ITagsService tags)
        {
            this.tags = tags;
        }

        [HttpPost]
        public ActionResult Index(string term)
        {
            return null;
        }
    }
}