using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eve_Calender
{

    public class MongoContext
    {
        MongoClient _client;        
        public IMongoDatabase Database;

        public MongoContext()
        {
            string mongoDb = Program.MONGO_DB;
            string mongoUsername = Program.MONGO_USERNAME;
            string mongoPassword = Program.MONGO_PASSWORD; // I don't care anymore, just make it work or something.
            int mongoPort = int.Parse(Program.MONGO_PORT);
            string mongoHost = Program.MONGO_HOST;

            Console.WriteLine(mongoHost);

            var credential = MongoCredential.CreateCredential(mongoDb, mongoUsername, mongoPassword);
            
            _client = new MongoClient($"mongodb://{mongoUsername}:{mongoPassword}@{mongoHost}"); //new MongoClient(settings);            
            Database = _client.GetDatabase(mongoDb);
            Console.WriteLine("Pinging mongodb");            
            Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            Console.WriteLine("Done mongodb");
            
        }
    }
}
