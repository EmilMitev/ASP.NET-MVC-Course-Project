namespace StackFaceSystem.Web.Areas.Administration.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IndexViewModel
    {
        public ICollection<UserViewModel> Users { get; set; }
    }
}
