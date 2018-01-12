namespace StackFaceSystem.Web.Areas.Administration.ViewModels.Users
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public ICollection<UserViewModel> Users { get; set; }
    }
}
