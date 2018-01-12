namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StackFaceSystem.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
