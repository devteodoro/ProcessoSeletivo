using ProcessoSeletivo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Api.ViewModels.User
{
    public class UserResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
    }
}
