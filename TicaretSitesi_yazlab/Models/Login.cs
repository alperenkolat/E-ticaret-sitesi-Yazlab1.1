using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TicaretSitesi_yazlab.Models
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [Required]
        public string? AdminName { get; set; }
        [Required]
        public string? AdminPassword { get; set; }
    }
}
