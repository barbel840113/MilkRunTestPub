using MilkRun.ApplicationCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MilkRun.Infrastructure.Repositories
{
    public interface IJsonRepository
    {
        Task<IEnumerable<Product>> GetAllJsonData();
    }

    internal class JsonRepository : IJsonRepository
    {
        public async Task<IEnumerable<Product>> GetAllJsonData()
        {
            string jsonString = await File.ReadAllTextAsync(Directory.GetCurrentDirectory() + "test_products.json"); // Load JSON content from file
            IEnumerable<Product> jsonItems = JsonSerializer.Deserialize<List<Product>>(jsonString); // Deserialize JSON content to objects
            return jsonItems;
        }
    }
}
