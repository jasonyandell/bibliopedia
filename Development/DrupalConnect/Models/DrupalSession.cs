namespace DrupalConnect.Models
{
    public class DrupalSession
    {
        public DrupalSession(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return Name + "=" + Id;
        }
    }
}