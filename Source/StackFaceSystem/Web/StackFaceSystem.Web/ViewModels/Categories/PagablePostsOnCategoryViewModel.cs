namespace StackFaceSystem.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using Posts;
    using StackFaceSystem.Data.Models;

    public class PagablePostsOnCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }
    }
}
