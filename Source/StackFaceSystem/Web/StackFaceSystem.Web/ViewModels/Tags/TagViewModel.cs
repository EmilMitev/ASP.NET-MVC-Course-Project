namespace StackFaceSystem.Web.ViewModels.Tags
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}
