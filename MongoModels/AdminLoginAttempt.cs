using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.MongoModels
{
    public class AdminLoginAttempt
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string? Username { get; set; } = string.Empty;

        public int? AdminId { get; set; } //can be null, handle with ?

        public bool Success { get; set; }

        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
    }
}
