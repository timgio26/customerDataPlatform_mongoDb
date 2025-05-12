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

        public async Task<Customer> GetAsync(string id)
        {
            var customer = await _customers.Find(x => x.Id == id).FirstOrDefaultAsync();
            return customer;
        }

        public async Task CreateAsync(Customer newCustomer)
        {
            await _customers.InsertOneAsync(newCustomer);
        }

        public async Task AddAddressAsync(string id, Address address)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var update = Builders<Customer>.Update.Push(x => x.AddressList, address);
            await _customers.UpdateOneAsync(filter, update);
        }

        public async Task AddServiceAsync(string customerId, string addressId, Service service)
        {
            var filter = Builders<Customer>.Filter.And(
                Builders<Customer>.Filter.Eq(x => x.Id, customerId),
                Builders<Customer>.Filter.ElemMatch(x => x.AddressList, a => a.Id == addressId)
            );
            var update = Builders<Customer>.Update.Push("AddressList.$.ServiceList", service);
            await _customers.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAddress(string customerId, string addressId)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customerId);
            var update = Builders<Customer>.Update.PullFilter(customer=>customer.AddressList,addressEntry => addressEntry.Id == addressId);
            await _customers.UpdateManyAsync(filter, update);
        }
    }
}
