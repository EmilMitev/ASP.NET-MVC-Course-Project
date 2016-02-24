namespace StackFaceSystem.Web.Areas.Administration.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
