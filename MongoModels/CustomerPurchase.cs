using ComputerStoreApplication.Models.Store;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.MongoModels
{
    public class CustomerPurchase
    {
        [BsonId]
        public ObjectId Id { get; set; }

        //Whom
        public int CustomerId { get; set; }

        //How much
        public decimal Price { get; set; }
        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
    }
}
