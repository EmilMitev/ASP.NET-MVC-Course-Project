namespace StackFaceSystem.Web.Areas.Moderator.ViewModels.ManageCategories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PagableSortableCategoriesViewModel
    {
        public string SortType { get; set; }

        public string SortDirection { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
