namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PagablePostsViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PostsViewModel> Posts { get; set; }
    }
}
