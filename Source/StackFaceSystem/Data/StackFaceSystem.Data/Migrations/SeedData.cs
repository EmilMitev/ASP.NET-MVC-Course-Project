namespace StackFaceSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StackFaceSystem.Common;
    using StackFaceSystem.Data.Models;

    public class SeedData
    {
        private static Random rand = new Random();

        public SeedData()
        {
            this.Roles = new List<IdentityRole>();
            this.Users = new List<ApplicationUser>();

            this.CreateRoles();
            this.CreateUsers();
        }

        public List<IdentityRole> Roles { get; private set; }

        public List<ApplicationUser> Users { get; private set; }

        private void CreateUsers()
        {
            ApplicationUser user;

            user = new ApplicationUser { UserName = "EMitev", Email = GlobalConstants.AdministratorUserName };
            this.Users.Add(user);

            for (int i = 1; i <= 5; i++)
            {
                // Create user
                var userName = string.Format("user{0}@user.com", i);
                var userPassword = userName;

                user = new ApplicationUser { UserName = userName, Email = userName };
                this.Users.Add(user);
            }
        }

        private void CreateRoles()
        {
            IdentityRole role;

            role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
            this.Roles.Add(role);

            role = new IdentityRole { Name = GlobalConstants.ModeratorRoleName };
            this.Roles.Add(role);

            role = new IdentityRole { Name = GlobalConstants.UserRoleName };
            this.Roles.Add(role);
        }

        private int Random(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
