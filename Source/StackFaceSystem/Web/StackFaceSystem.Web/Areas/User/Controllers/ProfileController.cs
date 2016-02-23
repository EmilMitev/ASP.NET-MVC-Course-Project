namespace StackFaceSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}