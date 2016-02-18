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
            this.Answers = new HashSet<Answer>();
            this.Ratings = new HashSet<RatingPost>();
            this.Tags = new HashSet<Tag>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<RatingPost> Ratings { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
