using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBMProject.ViewModels
{
    public class UserManagementAddRoleViewModel
    {

        public string UserId { get; set; }

        public string NewRole { get; set; }

        public SelectList Roles { get; set; }

        // Name ?
        public string Nome { get; set; }


    }
}
