namespace StackFaceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using StackFaceSystem.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public Comment()
        {
            this.Ratings = new HashSet<RatingComment>();
        }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        public virtual ICollection<RatingComment> Ratings { get; set; }
    }
}
