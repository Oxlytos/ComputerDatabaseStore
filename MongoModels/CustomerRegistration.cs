using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.MongoModels
{
    public class CustomerRegistration
    {
        public ObjectId Id { get; set; }
        //Whom
        public int CustomerId { get; set; }
        public string Email {  get; set; }
        //How much
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
