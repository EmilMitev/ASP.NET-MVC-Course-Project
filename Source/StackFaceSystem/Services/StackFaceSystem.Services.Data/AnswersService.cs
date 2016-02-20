namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class AnswersService : IAnswersService
    {
        private readonly IDbRepository<Answer> answers;

        public AnswersService(IDbRepository<Answer> answers)
        {
            this.answers = answers;
        }

        public IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take)
        {
            var answers = this.answers
                            .All()
                            .Where(x => x.PostId == postId)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((page - 1) * take)
                            .Take(take);

            return answers;
        }

        public int GetAnswerNumberPerPost(int postId)
        {
            return this.answers
                            .All()
                            .Where(x => x.PostId == postId)
                            .Count();
        }
    }
}
