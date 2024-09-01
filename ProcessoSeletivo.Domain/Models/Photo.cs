namespace ProcessoSeletivo.Domain.Models
{
    public class Photo : BaseEntity
    {
        public string Image { get; set; } = string.Empty;

        public bool Current { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; } = new();

        public Photo() { }

        public Photo(string image, bool current, int peopleId)
        {
            Image = image;
            Current = current;
            PersonId = peopleId;
        }
    }
}
