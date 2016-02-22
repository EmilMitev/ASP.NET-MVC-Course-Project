namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;
    using Common;
    public class AnswersService : IAnswersService
    {
        private readonly IDbRepository<Answer> answers;
        private readonly IIdentifierProvider identifierProvider;

        public AnswersService(IDbRepository<Answer> answers, IIdentifierProvider identifierProvider)
        {
            this.answers = answers;
            this.identifierProvider = identifierProvider;
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

        public void CreateAnswer(string postId, Answer answer)
        {
            var intPostId = this.identifierProvider.DecodeId(postId);
            answer.PostId = intPostId;
            this.answers.Add(answer);
            this.answers.Save();
        }
    }
}
