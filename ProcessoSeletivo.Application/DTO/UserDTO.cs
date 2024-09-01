using ProcessoSeletivo.Domain.Enums;

namespace ProcessoSeletivo.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public UserDTO() { }

        public UserDTO(string name, string password, Role role)
        {
            Name = name;
            Password = password;
            Role = role;
        }

        public UserDTO(int id, string name, string password, Role role)
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
        }

        public UserDTO(int id, string name, Role role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
