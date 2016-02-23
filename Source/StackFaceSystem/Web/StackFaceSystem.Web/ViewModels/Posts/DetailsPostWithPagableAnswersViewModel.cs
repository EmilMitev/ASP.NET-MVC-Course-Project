namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Answers;

    public class DetailsPostWithPagableAnswersViewModel
    {
        public DetailsPostViewModel Post { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<AnswersViewModel> Answers { get; set; }
    }
}
