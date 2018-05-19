using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza o seu email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza a sua password")]
        [MinLength(6, ErrorMessage = "A password deverá ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza o seu nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza o seu número institucional")]
        [MaxLength(9, ErrorMessage = "Numero deverá ser composto por 9 digitos")]
        [MinLength(9, ErrorMessage = "Numero deverá ser composto por 9 digitos")]
        [Display(Name = "Número")]
        public string Number { get; set; }
    }
}
