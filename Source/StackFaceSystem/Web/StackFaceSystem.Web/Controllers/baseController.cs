namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Common;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    }
}