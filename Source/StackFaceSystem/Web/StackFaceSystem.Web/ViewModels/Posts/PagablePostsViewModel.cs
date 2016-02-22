namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PagablePostsViewModel
    {
        public string SortField { get; set; }

        public string SortDirection { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }
    }
}
