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
            this.Ratings = new HashSet<Rating>();
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

        public int? CommentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Answer Comment { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
