using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.MongoModels
{
    public class CustomerLoginAttempt
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public int? CustomerId { get; set; } //can be null, handle with ?

        public bool Success { get; set; }

        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
    }
}
