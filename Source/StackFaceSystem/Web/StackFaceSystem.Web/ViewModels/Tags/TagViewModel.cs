namespace StackFaceSystem.Web.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}
