﻿namespace StackFaceSystem.Data.Migrations
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
            this.Votes = new List<Vote>();

            this.CreateRoles();
            this.CreateUsers();
            this.CreateCategories();
            this.CreateTags();
            this.CreatePosts();
            this.CreateAnswers();
            this.CreateComments();
            this.CreateVotes();
        }

        public List<IdentityRole> Roles { get; private set; }

        public List<ApplicationUser> Users { get; private set; }

        public List<Category> Categories { get; private set; }

        public List<Tag> Tags { get; private set; }

        public List<Post> Posts { get; private set; }

        public List<Answer> Answers { get; private set; }

        public List<Comment> Comments { get; private set; }

        public List<Vote> Votes { get; private set; }

        private void CreateVotes()
        {
            Vote vote;
            VoteValue value;

            for (int i = 0; i < this.Posts.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    switch (this.Random(0, 3))
                    {
                        case 0: value = VoteValue.Positive; break;
                        case 1: value = VoteValue.Negative; break;
                        default: value = VoteValue.Positive; break;
                    }

                    vote = new Vote
                    {
                        Value = value,
                        User = this.Users[randomUserNumber],
                        Post = this.Posts[i]
                    };

                    this.Votes.Add(vote);
                }
            }

            for (int i = 0; i < this.Answers.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    switch (this.Random(0, 3))
                    {
                        case 0: value = VoteValue.Positive; break;
                        case 1: value = VoteValue.Negative; break;
                        default: value = VoteValue.Positive; break;
                    }

                    vote = new Vote
                    {
                        Value = value,
                        User = this.Users[randomUserNumber],
                        Answer = this.Answers[i]
                    };

                    this.Votes.Add(vote);
                }
            }

            for (int i = 0; i < this.Comments.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    switch (this.Random(0, 3))
                    {
                        case 0: value = VoteValue.Positive; break;
                        case 1: value = VoteValue.Negative; break;
                        default: value = VoteValue.Positive; break;
                    }

                    vote = new Vote
                    {
                        Value = value,
                        User = this.Users[randomUserNumber],
                        Comment = this.Comments[i]
                    };

                    this.Votes.Add(vote);
                }
            }
        }

        private void CreateComments()
        {
            for (int i = 0; i < this.Answers.Count; i++)
            {
                var randomCommentsNumber = this.Random(0, 3);

                for (int j = 1; j <= randomCommentsNumber; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    var comment = new Comment()
                    {
                        Content = $"Answer{i} Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus at nisl aliquet, vulputate turpis et, lacinia tellus. Aliquam diam tortor, accumsan id interdum ac, convallis rhoncus enim. Proin sapien ipsum, fermentum consequat faucibus eu, aliquam at sapien. Morbi pretium quis tellus non convallis. Sed interdum aliquam auctor. Nulla tellus magna.",
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
                var randomAnswerNumber = this.Random(5, 10);

                for (int j = 1; j <= randomAnswerNumber; j++)
                {
                    var randomUserNumber = this.Random(0, this.Users.Count);

                    var answer = new Answer()
                    {
                        Content = $"Post{i}, Answer{j},Donec in elit tellus. Aenean vel consequat dolor. Nulla in nibh quis tellus suscipit interdum. Quisque feugiat pulvinar diam id aliquam. Cras ac nunc id turpis rutrum ultricies sed sed leo. Maecenas eleifend, mauris sed fringilla faucibus, neque justo laoreet est.",
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
                    Title = $"{i}This is post title",
                    Content = $"{i}Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse elementum nisl nec libero ullamcorper luctus. Donec facilisis nulla sed nulla facilisis pretium. Aenean congue dolor ut mauris interdum.",
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
