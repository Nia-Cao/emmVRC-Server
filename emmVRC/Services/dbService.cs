using emmVRC.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using static emmVRC.classes;

namespace emmVRC.Services

{
    public class dbService
    {
        private readonly IMongoCollection<tokens> _token;
        private readonly IMongoCollection<avatarClass> _avatar;
        private readonly IMongoCollection<loginKeys> _loginKeys;
        private readonly IMongoCollection<pins> _pins;

        private IMongoCollection<BsonDocument> _updateLoginKey;
        private IMongoCollection<BsonDocument> _updateToken;
        private IMongoCollection<BsonDocument> _updatePin;

        public dbService(IOptions<dbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
                dbSettings.Value.connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.databaseName);

            _token = mongoDatabase.GetCollection<tokens>("tokens");
            _loginKeys = mongoDatabase.GetCollection<loginKeys>("loginKeys");
            _pins = mongoDatabase.GetCollection<pins>("pins");
            _avatar = mongoDatabase.GetCollection<avatarClass>("avatars");

            _updateLoginKey = mongoDatabase.GetCollection<BsonDocument>("loginKeys");

            _updateToken = mongoDatabase.GetCollection<BsonDocument>("tokens");

            _updatePin = mongoDatabase.GetCollection<BsonDocument>("pins");
        }


        public async Task<tokens> GetAsync(string id) =>
            await _token.Find(x => x.token == id).FirstOrDefaultAsync();

        public async Task SetToken(string token, string user)
        {
            UpdateResult result = await _updateToken.UpdateOneAsync(
                                Builders<BsonDocument>.Filter.Eq("userid", user),
                                Builders<BsonDocument>.Update.Set("token", token),
                                options: new UpdateOptions { IsUpsert = true });
            Console.WriteLine(result.ModifiedCount.ToString());
        }

        public async Task<loginKeys> GetLoginKey(string id) =>
            await _loginKeys.Find(x => x.loginKey == id).FirstOrDefaultAsync();

        public async Task SetLoginKey(string key, string user)
        {
            UpdateResult result = await _updateLoginKey.UpdateOneAsync(
                                Builders<BsonDocument>.Filter.Eq("userid", user),
                                Builders<BsonDocument>.Update.Set("loginKey", key),
                                options: new UpdateOptions { IsUpsert = true });
            Console.WriteLine(result.ModifiedCount.ToString());
        }

        public async Task<pins> Getpin(string id) =>
            await _pins.Find(x => x.userid == id).FirstOrDefaultAsync();

        public async Task SetPin(string pin, string user)
        {
            var document = new BsonDocument {
                            { "userid", user },
                            { "pin", pin }
            };
            await _updatePin.InsertOneAsync(document);

            Console.WriteLine(user);
            Console.WriteLine(pin);
        }

        public async Task<avatarClass> GetAvatarInfo(string id) =>
            await _avatar.Find(x => x.avatar_id == id).FirstOrDefaultAsync();

        public async Task PutAvatar(avatarClass avatar)
        {
            var avatarBson = avatar.ToBsonDocument();

            await _avatar.InsertOneAsync(avatar);

        }

        public async Task<List<avatarClass>> SearchAvatar(string term)
        {
            var filterList = new List<FilterDefinition<avatarClass>>();

            filterList.Add(Builders<avatarClass>.Filter.Regex("avatar_author_name", term));
            filterList.Add(Builders<avatarClass>.Filter.Regex("avatar_name", term));
            var filters = Builders<avatarClass>.Filter.And(filterList);
            var result = _avatar.Find(Builders<avatarClass>.Filter.And(filterList)).ToList();
            return result;
        }
        public async Task<List<avatarClass>> GetAvatarAsync(string id)
        {
            var filter = Builders<avatarClass>.Filter.Eq("users", id);
            var projection = Builders<avatarClass>.Projection
                .Include("avatar_name").Include("avatar_id").Include("avatar_asset_url")
                .Include("avatar_thumbnail_image_url").Include("avatar_author_id").Include("avatar_id")
                .Include("avatar_supported_platforms");
            var result = _avatar.Find(filter).Project(projection).ToList();

            var list = new List<avatarClass>();

            foreach (var item in result)
            {
                var temp = BsonSerializer.Deserialize<avatarClass>(item);
                list.Add(temp);
            }

            return list;
        }
    }
}
