using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Practice_API.Classes;

namespace PracticeAPI_Test
{
    public static class TestDataBaseHelper
    {
        private const string TestConnectionString 
            = "server=localhost;port=3210;userid=root;password=khoa333;database=Animal";

        public static async Task<Animal> GetAnimalByName(string name)
        {
            await using var connection = new MySqlConnection(TestConnectionString);
            /*
            var foodDis = await  connection.QuerySingleAsync<Animal>
                (@"SELECT * FROM Animal WHERE NAME = @name", new {name});
               
            return await connection.QuerySingleAsync<Animal>
                (@"SELECT * FROM Animal WHERE NAME = @name", new {name});
            */
            return null;
        }

        public record Animal(string name);
    }
}