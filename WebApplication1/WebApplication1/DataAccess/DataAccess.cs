using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DataBaseAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "projectdb";
        private const string ClientCollection = "clients";
        private const string RecipientCollection = "recipients";
        private const string CarCollection = "cars";
        private const string OrderCollection = "orders";
        private const string ProductCollection = "products";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        // Clients

        public async Task<List<Client>> GetAllClients() => await ConnectToMongo<Client>(ClientCollection).Find(_ => true).ToListAsync();

        public async Task<Client?> GetClient(string id) => await ConnectToMongo<Client>(ClientCollection).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateClient(Client client) => await ConnectToMongo<Client>(ClientCollection).InsertOneAsync(client);

        public async Task UpdateClient(Client client, string id) => await ConnectToMongo<Client>(ClientCollection).ReplaceOneAsync(x => x.Id == id, client);

        public async Task RemoveClient(string id) => await ConnectToMongo<Client>(ClientCollection).DeleteOneAsync(x => x.Id == id);

        // Recipients

        public async Task<List<Recipient>> GetAllRecipients() => await ConnectToMongo<Recipient>(RecipientCollection).Find(_ => true).ToListAsync();

        public async Task<Recipient?> GetRecipient(string id) => await ConnectToMongo<Recipient>(RecipientCollection).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateRecipient(Recipient recipient) => await ConnectToMongo<Recipient>(RecipientCollection).InsertOneAsync(recipient);

        public async Task UpdateRecipient(Recipient recipient, string id) => await ConnectToMongo<Recipient>(RecipientCollection).ReplaceOneAsync(x => x.Id == id, recipient);

        public async Task RemoveRecipient(string id) => await ConnectToMongo<Recipient>(RecipientCollection).DeleteOneAsync(x => x.Id == id);

        // Cars

        public async Task<List<Car>> GetAllCars() => await ConnectToMongo<Car>(CarCollection).Find(_ => true).ToListAsync();

        public async Task<Car?> GetCar(string id) => await ConnectToMongo<Car>(CarCollection).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateCar(Car car) => await ConnectToMongo<Car>(CarCollection).InsertOneAsync(car);

        public async Task UpdateCar(Car car, string id) => await ConnectToMongo<Car>(CarCollection).ReplaceOneAsync(x => x.Id == id, car);

        public async Task RemoveCar(string id) => await ConnectToMongo<Car>(CarCollection).DeleteOneAsync(x => x.Id == id);

        // Products

        public async Task<List<Product>> GetAllProducts() => await ConnectToMongo<Product>(ProductCollection).Find(_ => true).ToListAsync();

        public async Task<Product?> GetProduct(string id) => await ConnectToMongo<Product>(ProductCollection).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateProduct(Product product) => await ConnectToMongo<Product>(ProductCollection).InsertOneAsync(product);

        public async Task UpdateProduct(Product product, string id) => await ConnectToMongo<Product>(ProductCollection).ReplaceOneAsync(x => x.Id == id, product);

        public async Task RemoveProduct(string id) => await ConnectToMongo<Product>(ProductCollection).DeleteOneAsync(x => x.Id == id);

        // Orders

        public async Task<List<Order>> GetAllOrders() => await ConnectToMongo<Order>(OrderCollection).Find(_ => true).ToListAsync();

        public async Task<Order?> GetOrder(string id) => await ConnectToMongo<Order>(OrderCollection).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateOrder(Order order) => await ConnectToMongo<Order>(OrderCollection).InsertOneAsync(order);

        public async Task UpdateOrder(Order order, string id) => await ConnectToMongo<Order>(OrderCollection).ReplaceOneAsync(x => x.Id == id, order);

        public async Task RemoveOrder(string id) => await ConnectToMongo<Order>(OrderCollection).DeleteOneAsync(x => x.Id == id);

    }
}
