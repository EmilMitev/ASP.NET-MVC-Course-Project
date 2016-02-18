namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using StackFaceSystem.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
