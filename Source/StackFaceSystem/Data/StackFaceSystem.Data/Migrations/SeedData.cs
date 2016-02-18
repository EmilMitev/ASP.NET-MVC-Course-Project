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
            this.Categories = new List<Category>();
            this.Tags = new List<Tag>();
            this.Posts = new List<Post>();
            this.Answers = new List<Answer>();
            this.Comments = new List<Comment>();

            this.CreateRoles();
            this.CreateUsers();
            this.CreateCategories();
            this.CreateTags();
            this.CreatePosts();
            this.CreateAnswers();
            this.CreateComments();
        }

        public List<IdentityRole> Roles { get; private set; }

        public List<ApplicationUser> Users { get; private set; }

        public List<Category> Categories { get; private set; }

        public List<Tag> Tags { get; private set; }

        public List<Post> Posts { get; private set; }

        public List<Answer> Answers { get; private set; }

        public List<Comment> Comments { get; private set; }

        private void CreateComments()
        {
            for (int i = 0; i < this.Answers.Count; i++)
            {
                var randomAnswerNumber = this.Random(0, 4);

                for (int j = 1; j <= randomAnswerNumber; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    var comment = new Comment()
                    {
                        Content = "Lorem ipsum dolor sit amet. Vivamus dapibus luctus nulla eget molestie. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam eget ullamcorper eros, aliquam varius sem. Vestibulum ultricies vehicula lectus, commodo bibendum leo",
                        User = this.Users[randomUserNumber],
                        Answer = this.Answers[i]
                    };

                    this.Comments.Add(comment);
                }
            }
        }

        private void CreateAnswers()
        {
            for (int i = 0; i < this.Posts.Count; i++)
            {
                var randomPostsNumber = this.Random(5, 10);

                for (int j = 1; j <= randomPostsNumber; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    var answer = new Answer()
                    {
                        Content = "Donec in elit tellus. Aenean vel consequat dolor. Nulla in nibh quis tellus suscipit interdum. Quisque feugiat pulvinar diam id aliquam. Cras ac nunc id turpis rutrum ultricies sed sed leo. Maecenas eleifend, mauris sed fringilla faucibus, neque justo laoreet est.",
                        User = this.Users[randomUserNumber],
                        Post = this.Posts[i]
                    };

                    this.Answers.Add(answer);
                }
            }
        }

        private void CreatePosts()
        {
            for (int i = 1; i <= 20; i++)
            {
                var randomUserNumber = this.Random(0, this.Users.Count);
                var randomCategoryNumber = this.Random(0, this.Categories.Count);

                var randomTags = new List<Tag>()
                {
                    this.Tags[this.Random(0, this.Tags.Count)],
                    this.Tags[this.Random(0, this.Tags.Count)],
                    this.Tags[this.Random(0, this.Tags.Count)]
                };

                var post = new Post
                {
                    Title = $"This is post title{i}",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse elementum nisl nec libero ullamcorper luctus. Donec facilisis nulla sed nulla facilisis pretium. Aenean congue dolor ut mauris interdum.",
                    User = this.Users[randomUserNumber],
                    Category = this.Categories[randomCategoryNumber],
                    Tags = randomTags
                };

                this.Posts.Add(post);
            }
        }

        private void CreateTags()
        {
            for (int i = 1; i <= 10; i++)
            {
                this.Tags.Add(new Tag { Name = $"Tag{i}" });
            }
        }

        private void CreateCategories()
        {
            for (int i = 1; i <= 10; i++)
            {
                this.Categories.Add(new Category { Name = $"Category{i}" });
            }
        }

        private void CreateUsers()
        {
            ApplicationUser user;

            user = new ApplicationUser { UserName = GlobalConstants.AdministratorUserName, Email = GlobalConstants.AdministratorUserName };
            this.Users.Add(user);

            user = new ApplicationUser { UserName = GlobalConstants.ModeratorUserName, Email = GlobalConstants.ModeratorUserName };
            this.Users.Add(user);

            for (int i = 1; i <= 5; i++)
            {
                // Create user
                var userName = $"user{i}@user.com";
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
