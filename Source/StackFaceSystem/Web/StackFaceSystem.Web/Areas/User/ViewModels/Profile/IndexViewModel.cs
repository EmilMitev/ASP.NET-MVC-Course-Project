namespace StackFaceSystem.Web.Areas.User.ViewModels.Profile
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string GoogleUrl { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string Adress { get; set; }
    }
}