namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Contracts.Data;
    using Services.Data;
    using ViewModels.Tags;

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