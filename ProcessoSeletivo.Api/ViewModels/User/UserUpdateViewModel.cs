using ProcessoSeletivo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.User
{
    public class UserUpdateViewModel
    {
        [Required]
        [Display(Name = "Id do usuário")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "O nome deve ter no mínimo 5 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "O campo deve ter entre 8 e 12 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,12}$", ErrorMessage = "O campo deve ser alfanumérico, ter entre 8 e 12 caracteres, e conter pelo menos uma letra e um número.")]
        public string Password { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Role Role { get; set; }
    }
}
