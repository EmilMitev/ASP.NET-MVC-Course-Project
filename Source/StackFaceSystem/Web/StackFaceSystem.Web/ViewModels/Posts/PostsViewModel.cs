namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Services.Common;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class PostsViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AnswersCount { get; set; }

        public int Votes { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public ICollection<string> Tags { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Posts/Details/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostsViewModel>().ForMember(x => x.AnswersCount, opt => opt.MapFrom(x => x.Answers.Count));
            configuration.CreateMap<Post, PostsViewModel>().ForMember(x => x.Votes, opt => opt.MapFrom(x => x.Votes.Count));
            configuration.CreateMap<Post, PostsViewModel>().ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
            configuration.CreateMap<Post, PostsViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Post, PostsViewModel>().ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Tags.Select(t => t.Name)));
        }
    }
}