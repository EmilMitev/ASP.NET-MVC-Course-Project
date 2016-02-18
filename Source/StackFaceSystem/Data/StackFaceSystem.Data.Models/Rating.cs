namespace StackFaceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;

    public class Rating : BaseModel<int>
    {
        public RatingValue Value { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int? AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        public int? PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}
