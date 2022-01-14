namespace Practice_API.Interfaces
{
    public interface IAnimal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsMale { get; set; }
    }
}