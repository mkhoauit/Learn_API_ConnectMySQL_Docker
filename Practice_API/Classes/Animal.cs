using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Practice_API.Interfaces;

namespace Practice_API.Classes
{
    public class Animal : IAnimal
    {
        [Key]
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsMale { get; set; }
        
        public List<FoodDistribution> FoodDistributions { get; set;}
    }
    
   
}