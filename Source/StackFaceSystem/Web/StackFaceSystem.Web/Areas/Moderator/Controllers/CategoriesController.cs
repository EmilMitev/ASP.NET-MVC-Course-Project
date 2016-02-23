namespace StackFaceSystem.Web.Areas.Moderator.Controllers
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
    public class CategoriesController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}