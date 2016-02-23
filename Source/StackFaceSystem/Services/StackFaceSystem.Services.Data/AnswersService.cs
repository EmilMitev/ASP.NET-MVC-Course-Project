namespace StackFaceSystem.Services.Data
{
    using System;
    using System.Linq;
    using Common;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class AnswersService : IAnswersService
    {
        private readonly IDbRepository<Answer> answers;

        public AnswersService(IDbRepository<Answer> answers)
        {
            this.answers = answers;
        }

        public Answer GetById(int id)
        {
            return this.answers.GetById(id);
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

        public void CreateAnswer(Answer answer)
        {
            this.answers.Add(answer);
            this.answers.Save();
        }

        public void DeleteAnswerByPostId(int postId)
        {
            var answers = this.answers.All().Where(x => x.PostId == postId).ToList();

            foreach (var answer in answers)
            {
                this.DeleteAnswer(answer);
            }
        }

        public void DeleteAnswer(Answer answer)
        {
            this.answers.Delete(answer);
            this.answers.Save();
        }

        public void UpdateAnswer(int answerId, string content)
        {
            var answer = this.answers.GetById(answerId);
            answer.Content = content;

            this.answers.Save();
        }
    }
}
