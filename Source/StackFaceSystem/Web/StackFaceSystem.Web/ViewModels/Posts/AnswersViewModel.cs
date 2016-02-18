namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Services.Common;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class AnswersViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public AnswersViewModel()
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

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int Rating { get; set; }

        public ICollection<CommentsOnAnswerViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, AnswersViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Post, AnswersViewModel>().ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Ratings.Any() ? x.Ratings.Where(r => r.AnswerId == this.Id).Sum(v => (int)v.Value) : 0));
            configuration.CreateMap<Post, AnswersViewModel>().ForMember(x => x.Answers, opt => opt.MapFrom(x => x.Answers.Where(a => a.Id == this.Id)));
        }
    }
}
