using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.Auth
{
    public class AuthViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Name { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
