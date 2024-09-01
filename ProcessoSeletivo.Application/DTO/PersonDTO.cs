using ProcessoSeletivo.Domain.Enums;

namespace ProcessoSeletivo.Application.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Sex { get; set; }

        public string Photo { get; set; }

        public PersonDTO() { }

        public PersonDTO(string name, string lastName, string cPF, DateTime dateOfBirth, Gender sex, string photo)
        {
            Name = name;
            LastName = lastName;
            CPF = cPF;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Photo = photo;
        }

        public PersonDTO(int id, string name, string lastName, string cpf, DateTime dateOfBirth, Gender sex, string photo)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CPF = cpf;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Photo = photo;
        }

        public PersonDTO(int id, string name, string lastName, string cpf, DateTime dateOfBirth, Gender sex)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CPF = cpf;
            DateOfBirth = dateOfBirth;
            Sex = sex;
        }
    }
}
