using CustomerDataPlatform.Models;
using MongoDB.Driver;

namespace CustomerDataPlatform.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IConfiguration config)
        {
            var client = new MongoClient(config["CdpDatabase:ConnectionString"]);
            var database = client.GetDatabase(config["CdpDatabase:DatabaseName"]);
            _customers = database.GetCollection<Customer>("customer");
        }

        public async Task<List<Customer>> GetAsync()
        {
            return await _customers.Find(_ => true).ToListAsync();
        }

        public async Task<Customer?> GetAsync(string id)
        {
            return await _customers.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Customer newCustomer)
        {
            await _customers.InsertOneAsync(newCustomer);
        }

        public async Task UpdateAsync(string id, Customer updatedCustomer)
        {
            await _customers.ReplaceOneAsync(x => x.Id == id, updatedCustomer);
        }

        public async Task RemoveAsync(string id)
        {
            await _customers.DeleteOneAsync(x => x.Id == id);
        }
    }
}
