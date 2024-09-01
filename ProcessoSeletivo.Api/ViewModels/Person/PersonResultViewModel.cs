using ProcessoSeletivo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.Person
{
    public class PersonResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Sex { get; set; }
    }
}
