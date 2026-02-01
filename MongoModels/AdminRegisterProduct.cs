using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.MongoModels
{
    public class AdminRegisterProduct
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int? AdminId { get; set; } //can be null, handle with ?

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
