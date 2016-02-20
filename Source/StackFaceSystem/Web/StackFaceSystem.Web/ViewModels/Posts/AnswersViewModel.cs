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

        public int VotesSum { get; set; }

        public ICollection<CommentsPerAnswerViewModel> CommentsOnAnswer { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswersViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Answer, AnswersViewModel>().ForMember(x => x.VotesSum, opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(v => (int)v.Value) : 0));
            configuration.CreateMap<Answer, AnswersViewModel>().ForMember(x => x.CommentsOnAnswer, opt => opt.MapFrom(x => x.Commets));
        }
    }
}
