using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicaretSitesi_yazlab.Models;

namespace TicaretSitesi_yazlab.Services
{
    public class LaptopService

    {


        private readonly IMongoCollection<Laptop> _LaptopsCollection;
        private readonly IMongoCollection<Login> _LoginsCollection;
        public LaptopService(IOptions<LaptopStoreDatabaseSettings> laptopStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
             laptopStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                laptopStoreDatabaseSettings.Value.DatabaseName);

            _LaptopsCollection = mongoDatabase.GetCollection<Laptop>(
               laptopStoreDatabaseSettings.Value.laptopsCollectionName);


            _LoginsCollection = mongoDatabase.GetCollection<Login>(
                   laptopStoreDatabaseSettings.Value.loginsCollectionName);
        }
        public async Task<List<Laptop>> GetWhereAsync(FilterDefinition<Laptop> matchStage) =>
          await _LaptopsCollection.Find(matchStage ,new FindOptions() { Collation = new Collation("en", strength: CollationStrength.Secondary) }).ToListAsync();
        public async Task<Login> loginControl(Login login) =>
         await _LoginsCollection.Find(x => x.AdminName == login.AdminName && x.AdminPassword == login.AdminPassword).FirstOrDefaultAsync();
        public async Task<List<Login>> login12() =>
            await _LoginsCollection.Find(_ => true) .ToListAsync();

        public async Task<List<Laptop>> SortGetAsync()
        {


            return await _LaptopsCollection.Find(_ => true).SortBy(x => x.Price).ToListAsync();

        }

        public async Task<List<Laptop>> SortRevGetAsync() =>
       await _LaptopsCollection.Find(_ => true).SortByDescending(x => x.Price).ToListAsync();
        public async Task<List<Laptop>> SortScoreGetAsync() =>
            await _LaptopsCollection.Find(_ => true).SortByDescending(x => x.Score).ToListAsync();

       

        public async Task<List<Laptop>> GetAsync() =>
            await _LaptopsCollection.Find(_ => true).ToListAsync();

        public async Task<Laptop?> GetAsync(string id) =>
            await _LaptopsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Laptop newLaptop) =>
            await _LaptopsCollection.InsertOneAsync(newLaptop);

        public async Task UpdateAsync(string id, Laptop updatedLaptop) =>
            await _LaptopsCollection.ReplaceOneAsync(x => x.Id == id, updatedLaptop);

        public async Task RemoveAsync(string id) =>
            await _LaptopsCollection.DeleteOneAsync(x => x.Id == id);

    }

}
