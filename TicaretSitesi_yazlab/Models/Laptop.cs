using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TicaretSitesi_yazlab.Models
{
    public class Laptop

    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public string? Brand { get; set; }
        public string? ModelName { get; set; }
        public string? ModelNo { get; set; }
        public string? OperatingSystem { get; set; }
        public string? OperatingGeneration { get; set; }
        public string? OperatingType { get; set; }
        public string? Ram { get; set; }
        public string? DiskSize { get; set; }
        public string? ScreenSize { get; set; }
        public float? Score { get; set; }
        public decimal? Price { get; set; }
        public string? Photo { get; set; }
        public string? Weblink { get; set; }
        public string? WebName { get; set; }
        public string? Svg { get; set; }

    }
}
