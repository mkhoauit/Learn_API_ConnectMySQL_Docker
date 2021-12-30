using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Practice_API.Classes;
using Practice_API.Controllers;
using Xunit;
using Shouldly;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Practice_API;
using Practice_API.Interfaces;

namespace PracticeAPI_Test
{
    
    public class Practice_API_Test
    {
        private readonly HttpClient _client;

        public Practice_API_Test()
        {
            var waf = new WebApplicationFactory<Startup>() ;
            _client = waf.CreateDefaultClient();
        }
        
        [Fact]
        public async Task Get_Wrong_ID_Of_Animal_Or_Food_or_FoodDistribution_ShouldBe()
        {
            //Arrange
            int idA = 99;
            int idF = 98;
        
            var response = await _client.GetAsync($"Animal/Get/{idA}");
            var response2 = await _client.GetAsync($"Food/Get/{idF}");
            var response3 = await _client.GetAsync($"FoodDistribution/Get/{idA}/{idF}");
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response2.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response3.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task Get_ID_NotExist_In_FoodDistribution_But_Exist_In_Animal_And_Food_ShouldBe()
        {
            //Arrange
            int idA = 2;
            int idF = 2;
        
            var response = await _client.GetAsync($"Animal/Get/{idA}");
            var response2 = await _client.GetAsync($"Food/Get/{idF}");
            var response3 = await _client.GetAsync($"FoodDistribution/Get/{idA}/{idF}");
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response2.StatusCode.ShouldBe(HttpStatusCode.OK);
            response3.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }
        
        [Fact]
        public async Task Get_Correct_ID_Of_Animal_Or_Food_Or_FoodDistribution_ShouldBe()
        {
            //Arrange
            int idA = 1;
            int idF = 1;
            
            var response = await _client.GetAsync($"Animal/Get/{idA}");
            var response2 = await _client.GetAsync($"Food/Get/{idF}");
            var response3 = await _client.GetAsync($"FoodDistribution/Get/{idA}/{idF}");
            //Action
            

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response2.StatusCode.ShouldBe(HttpStatusCode.OK);
            response3.StatusCode.ShouldBe(HttpStatusCode.OK);

        }
        
        [Fact]
        public async Task Post_Animal_ShouldBe()
        {
            //Arrange
            
            var response = 
                await _client.PostAsJsonAsync($"Animal/Post", new {  Name = "pop" ,Type = "dog" , IsMale = true });
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

        }
        
        [Fact]
        public async Task Post_Food_ShouldBe()
        {
            //Arrange
            var response = 
                await _client.PostAsJsonAsync($"Food/Post", new { FoodName = "vegetables" ,NumberofCans = 100 });
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }
        
        [Fact]
        public async Task Post_FoodDistribution_ShouldBe()
        {
            //Arrange
            var response = 
                await _client.PostAsJsonAsync($"FoodDistribution/Post", new { AnimalId = 2, FoodId = 1 , Quantity = 100, IsEnough = true});
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }
        
        [Fact]
        public async Task Put_WrongID_Animal_ShouldBe()
        {
            //Arrange
            int idA = 200;
            int idF = 300;

            var response = 
                await _client.PutAsJsonAsync($"Animal/Put/{idA}", new { Name = "Hyle" ,Type = "Cow" , IsMale = false });
            var response2 = 
                await _client.PutAsJsonAsync($"Food/Put/{idF}", new { FoodName = "tomatoes" ,NumberofCans = 500 });
            var response3 = 
                await _client.PutAsJsonAsync($"FoodDistribution/Put/{idA}/{idF}", new { Quantity = 900, IsEnough = false});
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response2.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response3.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            
        }
        
        [Fact]
        public async Task Put_CorrectID_Animal_ShouldBe()
        {
            //Arrange
            int id = 2;

            var response = 
                await _client.PutAsJsonAsync($"Animal/Put/{id}", new { Name = "Hylos" ,Type = "Cow" , IsMale = false });
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
       
        
        [Fact]
        public async Task Put_CorrectID_Food_ShouldBe()
        {
            //Arrange
            int id = 1 ;
            var response = 
                await _client.PutAsJsonAsync($"Food/Put/{id}", new { FoodName = "tomatoes" ,NumberofCans = 80 });
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Put_CorrectID_FoodDistribution_ShouldBe()
        {
            //Arrange
           
            int idA = 1;
            int idF = 1;
            var response = 
                await _client.PutAsJsonAsync($"FoodDistribution/Put/{idA}/{idF}", new { Quantity = 500, IsEnough = false});
           
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_NotExistID_FoodDistribution_But_Exist_In_Animal_And_Food_ShouldBe()
        {
            //Arrange
            int idA = 2;
            int idF = 3;
            var response = 
                await _client.PutAsJsonAsync($"FoodDistribution/Put/{idA}/{idF}", new { Quantity = 400, IsEnough = false});
            
            var checkAnimal = await _client.GetAsync($"Animal/Get/{idA}");
            var checkFood = await _client.GetAsync($"Food/Get/{idF}");;
       
            //Action

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            checkAnimal.StatusCode.ShouldBe(HttpStatusCode.OK);
            checkFood.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Delete_Animal_ShouldBe()
        {
            //Arrange
            int id = 4;

            //Action
            var action = await _client.DeleteAsync($"Animal/Delete/{id}");

            //Assert
           
            action.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Delete_Food_ShouldBe()
        {
            //Arrange
            int id = 3;

            //Action
            var action = await _client.DeleteAsync($"Food/Delete/{id}");

            //Assert
           
            action.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Delete_FoodDistribution_ShouldBe()
        {
            //Arrange
            int idA = 5;
            int idF = 3;

            //Action
            var action = await _client.DeleteAsync($"FoodDistribution/Delete/{idA}/{idF}");
            
            //Assert
           
            action.StatusCode.ShouldBe(HttpStatusCode.OK);
        }


    }
}