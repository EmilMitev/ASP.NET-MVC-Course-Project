namespace StackFaceSystem.Web.Controllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;

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