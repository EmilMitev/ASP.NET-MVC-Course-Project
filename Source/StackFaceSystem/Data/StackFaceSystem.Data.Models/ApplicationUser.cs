namespace StackFaceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string ImageUrl { get; set; }

        [MaxLength(1000)]
        public string FacebookUrl { get; set; }

        [MaxLength(1000)]
        public string TwitterUrl { get; set; }

        [MaxLength(1000)]
        public string GoogleUrl { get; set; }

        [MaxLength(1000)]
        public string LinkedInUrl { get; set; }

        [MaxLength(1000)]
        public string GitHubUrl { get; set; }

        [MaxLength(1000)]
        public string Adress { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
