namespace Practice_API.Interfaces
{
    public interface IFoodDistribution
    {
        public int AnimalId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public bool IsEnough { get; set; }

    }
}