using ProcessoSeletivo.Domain.Enums;
using System.Globalization;

namespace ProcessoSeletivo.Domain.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Sex { get; set; }

        public ICollection<Photo>? Photos { get; set; } = new List<Photo>();

        public Person() { }

        public Person(string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex)
        {
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;

            validation();
        }

        public Person(string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex, string photo)
        {
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;

            if (!string.IsNullOrEmpty(photo))
                Photos.Add(new Photo(photo, true, this.Id));

            validation();
        }

        public Person(int id, string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex, Photo photo)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Photos.Add(photo);

            validation();
        }

        public Person(int id, string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;

            validation();
        }

        private void validation()
        {
            if (this.Name.Length < 3 || this.Name.Length > 20)
                throw new Exception("O nome da pessoa deve conter de 3 a 20 caracteres!");

            if (this.LastName.Length < 3 || this.LastName.Length > 100)
                throw new Exception("O sobrenome da pessoa deve conter de 3 a 100 caracteres!");

            if(!(this.DateOfBirth > Convert.ToDateTime("1900-01-01") && this.DateOfBirth < DateTime.Now))
                throw new Exception($"O data de nascimento da pessoa deve estar entre 01/01/1900 e {DateTime.Now.ToString("dd/MM/yyyy")}!");
        }
    }
}
