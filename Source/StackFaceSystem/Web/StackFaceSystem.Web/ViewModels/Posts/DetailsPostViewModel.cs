namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Answers;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Common;

    public class DetailsPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public DetailsPostViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        public string EncodedId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.EncodeId(this.Id);
            }
        }

        public string Title { get; set; }

        public string SanitizedContent
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }
        
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public ICollection<string> Tags { get; set; }

        public int VotesSum { get; set; }

        public ICollection<AnswersViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, DetailsPostViewModel>().ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
            configuration.CreateMap<Post, DetailsPostViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
            configuration.CreateMap<Post, DetailsPostViewModel>().ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Tags.Select(t => t.Name)));
            configuration.CreateMap<Post, DetailsPostViewModel>().ForMember(x => x.VotesSum, opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(v => (int)v.Value) : 0));
            configuration.CreateMap<Post, DetailsPostViewModel>().ForMember(x => x.Answers, opt => opt.MapFrom(x => x.Answers));
        }
    }
}
