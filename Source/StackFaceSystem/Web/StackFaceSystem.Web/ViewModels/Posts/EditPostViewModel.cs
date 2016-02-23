namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Common;

    public class EditPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string EncodedId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.EncodeId(this.Id);
            }
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, EditPostViewModel>().ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}