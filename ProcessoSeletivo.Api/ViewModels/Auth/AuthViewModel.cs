using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.Auth
{
    public class AuthViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
