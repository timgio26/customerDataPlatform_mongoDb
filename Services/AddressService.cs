using CustomerDataPlatform.Models;
using MongoDB.Driver;

namespace CustomerDataPlatform.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _addresses;

        public AddressService(IConfiguration config)
        {
            var client = new MongoClient(config["CdpDatabase:ConnectionString"]);
            var database = client.GetDatabase(config["CdpDatabase:DatabaseName"]);
            _addresses = database.GetCollection<Address>("address");
        }

        public async Task CreateAsync(Address newAddress)
        {
            await _addresses.InsertOneAsync(newAddress);
        }
        public async Task<Address> GetAsync(string id)
        {
            return await _addresses.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
