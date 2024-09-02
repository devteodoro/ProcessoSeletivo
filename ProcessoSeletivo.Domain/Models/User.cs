using ProcessoSeletivo.Domain.Enums;
using System.Text.RegularExpressions;

namespace ProcessoSeletivo.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name {  get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Role Role { get; set; }

        public User() { }

        public User(string name, string password, Role role)
        {
            Name = name;
            Password = password;
            Role = role;
        }

        public User(int id, string name, string password, Role role)
        {
            Name = name;
            Password = password;
            Role = role;
        }
    }
}
