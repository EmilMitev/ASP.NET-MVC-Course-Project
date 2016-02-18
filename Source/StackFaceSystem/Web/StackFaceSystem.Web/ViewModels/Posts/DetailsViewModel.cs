namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Common;

    public class DetailsViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public DetailsViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string SanitizedContent
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public ICollection<string> Tags { get; set; }

        public int Rating { get; set; }

        public ICollection<AnswersViewModel> Answers { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, DetailsViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Post, DetailsViewModel>().ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
            configuration.CreateMap<Post, DetailsViewModel>().ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Tags.Select(t => t.Name)));
            configuration.CreateMap<Post, DetailsViewModel>().ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Ratings.Any() ? x.Ratings.Sum(v => (int)v.Value) : 0));
            configuration.CreateMap<Post, DetailsViewModel>().ForMember(x => x.Answers, opt => opt.MapFrom(x => x.Answers));
        }
    }
}
