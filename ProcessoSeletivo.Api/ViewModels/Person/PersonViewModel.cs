using ProcessoSeletivo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.Person
{
    public class PersonViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(20, ErrorMessage = "O nome deve ter no máximo 20 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(3, ErrorMessage = "O sobrenome deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O sobrenome deve ter no máximo 100 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "CPF inválido!")]
        public string CPF { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [EnumDataType(typeof(Gender))]
        public Gender Sex { get; set; }

        [Display(Name = "Foto")]
        public IFormFile? Photo { get; set; }
    }
}
