namespace StackFaceSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StackFaceSystem.Common;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                // For roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roleManager.Create(new IdentityRole { Name = GlobalConstants.AdministratorRoleName });
                roleManager.Create(new IdentityRole { Name = GlobalConstants.ModeratorRoleName });
                roleManager.Create(new IdentityRole { Name = GlobalConstants.UserRoleName });
            }
        }
    }
}