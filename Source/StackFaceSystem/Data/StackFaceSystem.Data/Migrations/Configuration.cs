namespace StackFaceSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using StackFaceSystem.Common;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var seed = new SeedData();

                // For roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                // For users
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add roles
                foreach (var role in seed.Roles)
                {
                    roleManager.Create(role);
                }

                // Add users and user to role
                foreach (var user in seed.Users)
                {
                    if (user.UserName == GlobalConstants.AdministratorUserName)
                    {
                        userManager.Create(user, GlobalConstants.AdministratorPassword);
                        userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
                    }
                    else if (user.UserName == GlobalConstants.ModeratorUserName)
                    {
                        userManager.Create(user, GlobalConstants.ModeratorPassword);
                        userManager.AddToRole(user.Id, GlobalConstants.ModeratorRoleName);
                    }
                    else
                    {
                        var userPassword = user.UserName;
                        userManager.Create(user, userPassword);
                        userManager.AddToRole(user.Id, GlobalConstants.UserRoleName);
                    }
                }

                // Add categories
                foreach (var category in seed.Categories)
                {
                    context.Categories.Add(category);
                }

                // Add tags
                foreach (var tag in seed.Tags)
                {
                    context.Tags.Add(tag);
                }

                // Add posts
                foreach (var post in seed.Posts)
                {
                    context.Posts.Add(post);
                }

                // Add answers
                foreach (var answer in seed.Answers)
                {
                    context.Answers.Add(answer);
                }

                // Add comments
                foreach (var comment in seed.Comments)
                {
                    context.Comments.Add(comment);
                }
            }
        }
    }
}
