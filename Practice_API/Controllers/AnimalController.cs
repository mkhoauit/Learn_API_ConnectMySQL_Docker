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
        
        // GET  Animal id 
        [HttpGet ("Animal/Get/{id}")]
        public async Task<ActionResult<Animal>> GetAnimalId(int id)
        {
            var existingAnimal = await _context.Animals.FindAsync(id);
            if (existingAnimal == null)
                return NotFound();

            return Ok(existingAnimal);
        }
        
        // GET ALL Food 
        [HttpGet ("Food/All")]
        public async Task<ActionResult<IEnumerable<Food>>> GetAllFood()
        {
            return await _context.Foods.ToListAsync();
        }
        
        // GET  Food id 
        [HttpGet ("Food/Get/{id}")]
        public async Task<ActionResult<Food>> GetFoodId(int id)
        {
            var existingFood = await _context.Foods.FindAsync(id);
            if (existingFood == null)
                return NotFound();

            return Ok( existingFood);
        }
        
        // GET ALL FoodDítribution 
        [HttpGet ("FoodDistribution/All")]
        public async Task<ActionResult<IEnumerable<FoodDistribution>>> GetAllFoodDistribution()
        {
            return await _context.FoodDistributions.ToListAsync();
        }
        
        // GET  FoodDistribution id 
        [HttpGet ("FoodDistribution/Get/{Animal_Id}/{Food_Id}")]
        public async Task<ActionResult<FoodDistribution>> GetFoodDistributionIdAnimal(int Animal_Id,int Food_Id)
        {
            var foodDis = _context.FoodDistributions.Where(p => p.AnimalId == Animal_Id)
                .FirstOrDefault<FoodDistribution>(distribution =>distribution.FoodId== Food_Id );
            var checkAnimalID = _context.Animals.FirstOrDefault(a=>a.AnimalId == Animal_Id);
            var checkFoodID = _context.Foods.FirstOrDefault(f => f.FoodId == Food_Id);

            if (foodDis is null)
            {
                if (checkAnimalID is null || checkFoodID is null)
                {
                    return NotFound();
                }
                
                return BadRequest();
            }

            var result= $"FoodDistribution AnimalId: {foodDis.AnimalId}, idF: {foodDis.FoodId}, quantity: {foodDis.Quantity}, IsEnough:{foodDis.IsEnough}";
            return Ok(result);
        }
        
        // POST Animal
        [HttpPost("Animal/Post")]
        public async Task<ActionResult<AnimalDto>> CreateAnimal(AnimalInputDto input)
        {
            
            var inputAnimal = new AnimalDto()
            {
                Name = input.Name,
                Type = input.Type,
                IsMale = input.IsMale
            };

            var animal = new Animal()
            {
                Name = inputAnimal.Name,
                Type = inputAnimal.Type,
                IsMale = inputAnimal.IsMale
            };

            _context.Animals.Add(animal);

            _context.SaveChanges();
            
            
            return CreatedAtAction(nameof(CreateAnimal)
                , new {AnimalId = animal.AnimalId, name = animal.Name, type= animal.Type, isMale= animal.IsMale }, animal);
          
        }
        
        // POST Food
        [HttpPost("Food/Post")]
        public async Task<ActionResult<FoodDto>> CreateFood(FoodInputDto input)
        {
            var inputFood = new FoodDto()
            {
                FoodName = input.FoodName,
                NumberofCans = input.NumberofCans
            };

            var food = new Food()
            {
                FoodName = inputFood.FoodName,
                NumberofCans = inputFood.NumberofCans
                
            };

            _context.Foods.Add(food);
            
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreateFood), 
                new { food.FoodId, food.FoodName, food.NumberofCans}, food);
        }
        
        
        // POST FoodDistribution
        [HttpPost("FoodDistribution/Post")]
        public async Task<ActionResult<FoodDistributionDto>> CreateFoodDistribution(FoodDistributionDto input)
        {
            var checkAnimal = _context.Animals.SingleOrDefault(a => a.AnimalId == input.AnimalId);
            var checkFood = _context.Foods.SingleOrDefault(f => f.FoodId == input.FoodId);
            if (checkAnimal is null || checkFood is null)
                return NotFound();

            var inputDis = new FoodDistributionDto()
            {
                AnimalId = input.AnimalId,
                FoodId = input.FoodId,
                Quantity = input.Quantity,
                IsEnough = input.IsEnough
            };
            
            var foodDistribution= new FoodDistribution()
            {
                AnimalId = inputDis.AnimalId,
                FoodId = inputDis.FoodId,
                Quantity = inputDis.Quantity,
                IsEnough = inputDis.IsEnough
            };
            _context.FoodDistributions.Add(foodDistribution);
            _context.SaveChanges();
            return  CreatedAtAction(nameof(CreateFoodDistribution), 
                new {inputDis.AnimalId, inputDis.FoodId, inputDis.Quantity,inputDis.IsEnough},inputDis);
        }
        
        
        // PUT Animal
        [HttpPut("Animal/Put/{id}")]
        public async Task<ActionResult<AnimalDto>> PutAnimal(int id, AnimalInputDto input)
        {
          
            var animal = _context.Animals.Where(p => p.AnimalId == id).FirstOrDefault<Animal>();
            if (animal is null)
                return NotFound();

            animal.Name = input.Name;
            animal.Type = input.Type;
            animal.IsMale = input.IsMale;
            _context.SaveChanges();

            return Ok($"Successfully update AnimalId: {animal.AnimalId}, name: {animal.Name}, type: {animal.Type}, IsMale:{animal.IsMale}");
            
        }
        
        // PUT Food
        [HttpPut("Food/Put/{id}")]
        public async Task<ActionResult<FoodDto>> PutFood(int id, FoodInputDto input)
        {
            var food = _context.Foods.Where(p => p.FoodId == id).FirstOrDefault<Food>();
            if (food is null)
                return NotFound();

            food.FoodName = input.FoodName;
            food.NumberofCans = input.NumberofCans;
            _context.SaveChanges();

            return Ok($"Successfully update FoodId: {food.FoodId}, name: {food.FoodName}, Quantity: {food.NumberofCans}");
        }
        
        // PUT FoodDistribution
        [HttpPut("FoodDistribution/Put/{idA}/{idF}")]
        public async Task<ActionResult<FoodDistributionDto>> PutFoodDistribution(int idA,int idF, FoodDistributionUpdateDto input)
        {
            var foodDis = _context.FoodDistributions.Where(p => p.AnimalId == idA)
                .FirstOrDefault<FoodDistribution>(distribution =>distribution.FoodId==idF );
            
            if (foodDis is null)
                return NotFound();
            if (foodDis.FoodId != idF)
                return BadRequest();

            foodDis.Quantity = input.Quantity;
            foodDis.IsEnough = input.IsEnough;
            _context.SaveChanges();
            
            return Ok($"Successfully update FoodDistribution: AnimalId: {foodDis.AnimalId} FoodId: {foodDis.FoodId}, Quantity: {foodDis.Quantity}, IsEnough: {foodDis.IsEnough}");

        }

        // DELETE Animal
        [HttpDelete("Animal/Delete/{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var delAnimal = await  _context.Animals.FindAsync(id);

            if (delAnimal is null)
                return NotFound();

            _context.Animals.Remove(delAnimal);
            _context.SaveChanges();

            return Ok($"Succecfully DELETE AnimalId: {delAnimal.AnimalId}; nameAnimal: {delAnimal.Name}");
        }
        
        // DELETE Food
        [HttpDelete("Food/Delete/{id}")]
        public async Task<ActionResult<Food>> DeleteFood(int id)
        {
            var delFood = await  _context.Foods.FindAsync(id);

            if (delFood is null)
                return NotFound();

            _context.Foods.Remove(delFood);
            _context.SaveChanges();

            return Ok($"Succecfully DELETE FoodId:{delFood.FoodId}; nameFood: {delFood.FoodName} ");
        }
        
        // DELETE FoodDistribution
        [HttpDelete("FoodDistribution/Delete/{idA}/{idF}")]
        public async Task<ActionResult<FoodDistributionDto>> DelFoodDistribution(int idA,int idF)
        {
            var foodDis = _context.FoodDistributions.Where(p => p.AnimalId == idA)
                .FirstOrDefault<FoodDistribution>(distribution =>distribution.FoodId==idF );
            
            if (foodDis is null  )
                return NotFound();
            if (foodDis.FoodId != idF)
                return BadRequest();
            
            _context.FoodDistributions.Remove(foodDis);
            _context.SaveChanges();
            return Ok($"Successfully remove FoodDistribution idA: {foodDis.AnimalId}, idF: {foodDis.FoodId}, quantity: {foodDis.Quantity}, IsEnough:{foodDis.IsEnough}");
        }
    }
}
