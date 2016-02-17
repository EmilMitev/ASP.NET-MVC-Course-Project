namespace StackFaceSystem.Web.Areas.Administrator
{
    using System.Web.Mvc;

    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}