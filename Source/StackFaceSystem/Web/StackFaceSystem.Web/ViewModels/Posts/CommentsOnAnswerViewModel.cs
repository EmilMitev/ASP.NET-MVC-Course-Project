namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Linq;
    using AutoMapper;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Common;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class CommentsOnAnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public CommentsOnAnswerViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        public string SanitizedContent
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, CommentsOnAnswerViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Post, CommentsOnAnswerViewModel>().ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Ratings.Any() ? x.Ratings.Sum(v => (int)v.Value) : 0));
        }
    }
}
