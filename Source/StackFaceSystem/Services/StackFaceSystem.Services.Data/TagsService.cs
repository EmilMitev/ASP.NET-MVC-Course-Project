namespace StackFaceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDbRepository<Tag> tags;

        public TagsService(IDbRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable<Tag> CheckExist(string tags)
        {
            var tagsAsList = tags.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            return null;
        }
    }
}
