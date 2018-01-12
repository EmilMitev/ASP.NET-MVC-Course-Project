namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class AnswersService : IAnswersService
    {
        private readonly IDbRepository<Answer> m_Answers;

        public AnswersService(IDbRepository<Answer> answers)
        {
            m_Answers = answers;
        }

        public void CreateAnswer(Answer answer)
        {
            m_Answers.Add(answer);
            m_Answers.Save();
        }

        public Answer GetById(int id)
        {
            return m_Answers.GetById(id);
        }

        public IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take)
        {
            return m_Answers
                        .All()
                        .Where(x => x.PostId == postId)
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip((page - 1) * take)
                        .Take(take);
        }

        public int GetAnswerCountPerPost(int postId)
        {
            return m_Answers
                        .All()
                        .Count(x => x.PostId == postId);
        }

        public void UpdateAnswer(int answerId, string content)
        {
            var answer = m_Answers.GetById(answerId);
            answer.Content = content;

            m_Answers.Save();
        }

        public void DeleteAnswerByPostId(int postId)
        {
            var answers = m_Answers
                            .All()
                            .Where(x => x.PostId == postId)
                            .ToList();

            foreach (var answer in answers)
            {
                DeleteAnswer(answer);
            }
        }

        public void DeleteAnswer(Answer answer)
        {
            m_Answers.Delete(answer);
            m_Answers.Save();
        }
    }
}
