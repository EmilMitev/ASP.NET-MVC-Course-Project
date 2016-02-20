using StackFaceSystem.Data.Models;
using StackFaceSystem.Services.Common;
using StackFaceSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace StackFaceSystem.Web.ViewModels.Posts
{
    public class CommentsPerAnswerViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public CommentsPerAnswerViewModel()
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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentsPerAnswerViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Comment, CommentsPerAnswerViewModel>().ForMember(x => x.VotesSum, opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(v => (int)v.Value) : 0));
        }
    }
}
