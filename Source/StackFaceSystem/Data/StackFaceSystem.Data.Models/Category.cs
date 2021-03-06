﻿namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StackFaceSystem.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
