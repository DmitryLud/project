using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public decimal Price { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string? CarId { get; set; } = null;

        public string? ProductId { get; set; } = null;

        public string? ClientId { get; set; } = null;

        public string? RecipientId { get; set; } = null;

        public double Weight { get; set; }
    }
}
