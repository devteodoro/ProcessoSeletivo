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

            validName();
            validPassword();
        }

        public User(int id, string name, string password, Role role)
        {
            Name = name;
            Password = password;
            Role = role;

            validName();
            validPassword();
        }

        public void validName()
        {
            if (this.Name.Length <= 5)
                throw new Exception("O nome deve conter pelo menos 5 caracteres!");
        }

        public void validPassword()
        {
            // Verifica se a senha é nula ou vazia
            if (string.IsNullOrEmpty(this.Password))
                throw new Exception("A senha não pode ser vazia!");

            // Verifica se a senha possui entre 8 e 12 caracteres
            if (this.Password.Length < 8 || this.Password.Length > 12)
                throw new Exception("A senha deve conter entre 8 e 12 caracteres!");

            // Verifica se a senha é alfanumérica
            if (!Regex.IsMatch(this.Password, @"^[a-zA-Z0-9]+$"))
                throw new Exception("A senha deve ser alfanumérica!");

            // Verifica se a senha possui ao menos uma letra e um número
            if (!Regex.IsMatch(this.Password, @"[a-zA-Z]") || !Regex.IsMatch(this.Password, @"\d"))
                throw new Exception("A senha deve conter letras e números!");
        }
    }
}
