using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace emmVRC.Models
{
    public class tokens
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string userid { get; set; } = null!;

        public string username { get; set; } = null!;

        public double expires { get; set; }

        public string token { get; set; } = null!;
    }

    public class loginKeys
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string userid { get; set; } = null!;

        public DateTime expires { get; set; }

        public string loginKey { get; set; }
    }

    public class pins
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string userid { get; set; } = null!;

        public string pin { get; set; }

    }
}
