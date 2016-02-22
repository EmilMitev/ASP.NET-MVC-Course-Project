namespace StackFaceSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface ITagsService
    {
        IQueryable<Tag> CheckExist(string tags);

        IQueryable<Tag> GetAllTags();
    }
}