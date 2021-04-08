namespace EFRelationships.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}