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
            Votes = new HashSet<Vote>();
            Commets = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Commets { get; set; }
    }
}
