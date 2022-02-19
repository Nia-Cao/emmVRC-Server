using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace emmVRC
{
    public class classes
    {

		public class avatarClass
		{
			[JsonIgnore]
			[BsonId]
			public ObjectId id { get; set; }
			public string avatar_name { get; set; }
			public string avatar_id { get; set; }
			public string avatar_asset_url { get; set; }
			public string avatar_thumbnail_image_url { get; set; }
			public string avatar_author_id { get; set; }
			public string avatar_author_name { get; set; }
			public string avatar_category { get; set; }
			public int avatar_public { get; set; }
			public int avatar_supported_platforms { get; set; }

			[JsonIgnore]
			public string[] users { get; set; }
		}

		public class avatarClassStripped
		{
			public string avatar_name { get; set; }

			public string avatar_id { get; set; }

			public string avatar_asset_url { get; set; }

			public string avatar_thumbnail_image_url { get; set; }

			public string avatar_author_id { get; set; }

			public int avatar_supported_platforms { get; set; }
		}
	}

}
