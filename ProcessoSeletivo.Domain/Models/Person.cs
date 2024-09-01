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
        }

        public Person(int id, string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;
        }
    }
}
