using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_API.Classes;

namespace Practice_API.Controllers
{
    [ApiController]
    
    public class AnimalController : Controller
    {
        private readonly  AnimalContext _context;
        
        
        public AnimalController(AnimalContext context)
        {
            _context = context;
        }
        
        // GET ALL Animal 
        [HttpGet ("Animal/All")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAllAnimal()
        {
            return await _context.Animals.ToListAsync();
        }
        
        // GET ALL Food 
        [HttpGet ("Food/All")]
        public async Task<ActionResult<IEnumerable<Food>>> GetAllFood()
        {
            return await _context.Foods.ToListAsync();
        }
        
        // GET ALL FoodDítribution 
        [HttpGet ("FoodDistribution/All")]
        public async Task<ActionResult<IEnumerable<FoodDistribution>>> GetAllFoodDistribution()
        {
            return await _context.FoodDistributions.ToListAsync();
        }
        
        // POST Animal
        [HttpPost("Animal/Post")]
        public async Task<ActionResult<Animal>> CreateAnimal(Animal animal)
        {
            var existingAnimal = _context.Animals.SingleOrDefault(a => a.AnimalId == animal.AnimalId);
            if (existingAnimal is not null)
            {
                return BadRequest();

            }
            _context.Animals.Add(new Animal()
            {
                AnimalId = animal.AnimalId,
                Name = animal.Name,
                Type = animal.Type,
                IsMale = animal.IsMale
                
            });
            _context.SaveChanges();
            return Content($"Successfully post AnimalId: {animal.AnimalId}, name: {animal.Name}, type: {animal.Type}, IsMale:{animal.IsMale}");
        }
        
        // POST Food
        [HttpPost("Food/Post")]
        public async Task<ActionResult<Food>> CreateFood(Food food)
        {
            var existingFood = _context.Foods.SingleOrDefault(a => a.FoodId == food.FoodId);
            if (existingFood is not null)
            {
                return BadRequest();

            }
            _context.Foods.Add(new Food()
            {
                FoodId = food.FoodId,
                FoodName = food.FoodName,
                NumberofCans = food.NumberofCans
            });
            _context.SaveChanges();
            return Content($"Successfully post FoodId: {food.FoodId}, name: {food.FoodName}, Quantity: {food.NumberofCans}");
        }
        
        
        // POST FoodDistribution
        [HttpPost("FoodDistribution/Post")]
        public async Task<ActionResult<FoodDistribution>> AddFoodDistribution(FoodDistribution foodDistribution)
        {
            var checkAnimal = _context.Animals.SingleOrDefault(a => a.AnimalId == foodDistribution.AnimalId);
            var checkFood = _context.Foods.SingleOrDefault(f => f.FoodId == foodDistribution.FoodId);
            var existingDisA = _context.FoodDistributions.SingleOrDefault(d => d.AnimalId == foodDistribution.AnimalId);
            var existingDisF = _context.FoodDistributions.SingleOrDefault(d => d.FoodId == foodDistribution.FoodId);
            
            if (checkAnimal is null || checkFood is null)
            {
                return NotFound();
            }
            if (existingDisA is not null && existingDisF is not null)
            {
                return BadRequest();
            }
            
            _context.FoodDistributions.Add(new FoodDistribution()
            {
                AnimalId = foodDistribution.AnimalId,
                FoodId = foodDistribution.FoodId,
                Quantity = foodDistribution.Quantity,
                IsEnough = foodDistribution.IsEnough
            });
            _context.SaveChanges();
            return Content($"Successfully post FoodDistribution idA: {foodDistribution.AnimalId}, idF: {foodDistribution.FoodId}, quantity: {foodDistribution.Quantity}, IsEnough:{foodDistribution.IsEnough}");
        }
        
        
        // PUT Animal
        [HttpPut("Animal/Put")]
        public async Task<ActionResult<Animal>> PutAnimal(int id, Animal animal)
        {
            
            if (id != animal.AnimalId)
                return BadRequest();
            
            var existingAnimal = _context.Animals.Where(p => p.AnimalId == animal.AnimalId).FirstOrDefault<Animal>();
            if (existingAnimal is null)
                return NotFound();
            
            existingAnimal.Name = animal.Name;
            existingAnimal.Type = animal.Type;
            existingAnimal.IsMale = animal.IsMale;
            _context.SaveChanges();

            return Content($"Successfully update AnimalId: {animal.AnimalId}, name: {animal.Name}, type: {animal.Type}, IsMale:{animal.IsMale}");
            
        }
        
        // PUT Food
        [HttpPut("Food/Put")]
        public async Task<ActionResult<Food>> PutFood(int id, Food food)
        {
            
            if (id != food.FoodId)
                return BadRequest();
            
            var existingFood = _context.Foods.Where(p => p.FoodId == food.FoodId).FirstOrDefault<Food>();
            if (existingFood is null)
                return NotFound();
            
            existingFood.FoodName = food.FoodName;
            existingFood.NumberofCans = food.NumberofCans;
            _context.SaveChanges();

            return Content($"Successfully update FoodId: {food.FoodId}, name: {food.FoodName}, Quantity: {food.NumberofCans}");
        }
        
        // PUT Food
        [HttpPut("FoodDistribution/Put")]
        public async Task<ActionResult<Food>> PutFoodDistribution(int idA,int idF, FoodDistribution foodDistribution)
        {
            if (idA == foodDistribution.AnimalId)
            {
                if (idF == foodDistribution.FoodId)
                {
                   var existingDis = _context.FoodDistributions.Where(p => p.AnimalId == foodDistribution.AnimalId).FirstOrDefault<FoodDistribution>();
                   existingDis.Quantity = foodDistribution.Quantity;
                   existingDis.IsEnough = foodDistribution.IsEnough;
                   _context.SaveChanges();
                   return Content($"Successfully update FoodDistribution idA: {existingDis.AnimalId}, idF: {existingDis.FoodId}, quantity: {existingDis.Quantity}, IsEnough:{existingDis.IsEnough}");
                }
                return NotFound();
            }
            return NotFound();
        }
        
        
        // DELETE Animal
        [HttpDelete("Animal/Delete")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var delAnimal = await  _context.Animals.FindAsync(id);

            if (delAnimal is null)
                return NotFound();

            _context.Animals.Remove(delAnimal);
            _context.SaveChanges();

            return Content($"Succecfully DELETE AnimalId: {delAnimal.AnimalId}; nameAnimal: {delAnimal.Name}");
        }
        
        // DELETE Food
        [HttpDelete("Food/Delete")]
        public async Task<ActionResult<Food>> DeleteFood(int id)
        {
            var delFood = await  _context.Foods.FindAsync(id);

            if (delFood is null)
                return NotFound();

            _context.Foods.Remove(delFood);
            _context.SaveChanges();

            return Content($"Succecfully DELETE FoodId:{delFood.FoodId}; nameFood: {delFood.FoodName} ");
        }
        
        // DELETE FoodDistribution
        [HttpDelete("FoodDistribution/Delete")]
        public async Task<ActionResult<FoodDistribution>> DelFoodDistribution(int id)
        {
            var checkDistribution =  await  _context.FoodDistributions.FindAsync(id);
           
            if (checkDistribution is null )
            {
                return NotFound();
            }
            _context.FoodDistributions.Remove(checkDistribution);
            _context.SaveChanges();
            return Content($"Successfully remove FoodDistribution idA: {checkDistribution.AnimalId}, idF: {checkDistribution.FoodId}, quantity: {checkDistribution.Quantity}, IsEnough:{checkDistribution.IsEnough}");
        }
    }
}
