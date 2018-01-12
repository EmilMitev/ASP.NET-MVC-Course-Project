namespace StackFaceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDbRepository<Tag> m_Tags;

        public TagsService(IDbRepository<Tag> tags)
        {
            m_Tags = tags;
        }

        public IQueryable<Tag> CheckExist(string tags)
        {
            var tagsAsList = tags.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var tagsList = new List<Tag>();

            foreach (var tag in tagsAsList)
            {
                Tag tagFromDb = m_Tags.All().FirstOrDefault(x => x.Name == tag);
                if (tagFromDb == null)
                {
                    var tagToAdd = new Tag
                    {
                        Name = tag
                    };

                    m_Tags.Add(tagToAdd);
                    m_Tags.Save();
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
            return m_Tags.All().OrderBy(x => x.Name);
        }
    }
}
