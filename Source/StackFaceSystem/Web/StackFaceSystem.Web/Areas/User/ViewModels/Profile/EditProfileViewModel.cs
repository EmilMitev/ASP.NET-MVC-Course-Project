namespace StackFaceSystem.Web.Areas.User.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class EditProfileViewModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        [MaxLength(1000)]
        [RegularExpression(@"[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}", ErrorMessage = "Invalid image url")]
        public string ImageUrl { get; set; }

        [MaxLength(1000)]
        [RegularExpression(@"(?:(?:http|https):\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?", ErrorMessage = "Invalid facebook url")]
        public string FacebookUrl { get; set; }

        [MaxLength(1000)]
        [RegularExpression(@"(?:http:\/\/)?(?:www\.)?twitter\.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[\w\-]*\/)*([\w\-]*)", ErrorMessage = "Invalid twitter url")]
        public string TwitterUrl { get; set; }

        [MaxLength(1000)]
        public string GoogleUrl { get; set; }

        [MaxLength(1000)]
        public string LinkedInUrl { get; set; }

        [MaxLength(1000)]
        public string GitHubUrl { get; set; }

        [MaxLength(100)]
        public string Adress { get; set; }
    }
}