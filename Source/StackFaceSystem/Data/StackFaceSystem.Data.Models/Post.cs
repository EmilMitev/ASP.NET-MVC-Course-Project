namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using StackFaceSystem.Data.Common.Models;

    public class Post : BaseModel<int>
    {
        public Post()
        {
            Answers = new HashSet<Answer>();
            Votes = new HashSet<Vote>();
            Tags = new HashSet<Tag>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        [Required]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
