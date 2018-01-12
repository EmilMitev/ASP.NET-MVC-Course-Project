namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;

    [Authorize]
    public class TagsController : BaseController
    {
        private readonly ITagsService m_Tags;

        public TagsController(ITagsService tags)
        {
             m_Tags = tags;
        }

        [HttpPost]
        public ActionResult Index(string term)
        {
            return null;
        }
    }
}