using System.ComponentModel.DataAnnotations;
using Practice_API.Interfaces;

namespace Practice_API.Classes
{
    public class FoodDistribution : IFoodDistribution
    {
        public int AnimalId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public bool IsEnough { get; set; }
        
        public Animal Animal { get; set; }
        public Food Food { get; set; }
    }
}