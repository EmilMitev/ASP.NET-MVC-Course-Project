namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;

    public class Answer : BaseModel<int>
    {
        public Answer()
        {
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<RatingAnswer>();
        }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<RatingAnswer> Ratings { get; set; }
    }
}
