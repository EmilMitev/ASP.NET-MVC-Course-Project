namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using StackFaceSystem.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
