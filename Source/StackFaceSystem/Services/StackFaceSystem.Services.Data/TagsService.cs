namespace StackFaceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;
    using Contracts;
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

            var tagsList = new List<Tag>();

            foreach (var tag in tagsAsList)
            {
                Tag tagFromDb = this.tags.All().FirstOrDefault(x => x.Name == tag);
                if (tagFromDb == null)
                {
                    var tagToAdd = new Tag
                    {
                        Name = tag
                    };

                    this.tags.Add(tagToAdd);
                    this.tags.Save();
                    tagsList.Add(tagToAdd);
                }
                else
                {
                    tagsList.Add(tagFromDb);
                }
            }

            return tagsList.OrderBy(x => x.Name).AsQueryable();
        }

        public IQueryable<Tag> GetAllTags()
        {
            return this.tags.All().OrderBy(x => x.Name);
        }
    }
}
