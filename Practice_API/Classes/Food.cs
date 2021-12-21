using System.Collections.Generic;
using Practice_API.Interfaces;

namespace Practice_API.Classes
{
    public class Food : IFood
    {
        public int FoodId { get; set; }
        public string FoodName { get; set;}
        public int NumberofCans { get; set; }
        public List<FoodDistribution> FoodDistributions { get; set;}

    }
}