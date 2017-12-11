using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using SampleData.DAL.Cobb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleData.DAL
{
    public class SampleDbContext
    {
        public SampleDbContext()
        {
            Client = new MongoClient("mongodb://localhost:27017");
            Database = Client.GetDatabase("sampledata");
            CobbLogs = Database.GetCollection<Log>("cobblogs");
        }
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<Log> CobbLogs { get; set; }

        public bool AddLog(Log logToAdd)
        {
            try
            {
                this.CobbLogs.InsertOne(logToAdd);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public List<Log> GetLogInfos()
        {
            //var projectFilter = new FilterDefinitionBuilder<Log>();
            
            return CobbLogs.Find<Log>(new BsonDocument())
                .Project(l => new Log { LoggerVersionInfo = l.LoggerVersionInfo, MapInfo = l.MapInfo, VehicleInfo = l.VehicleInfo, Id = l.Id })
                .ToList();
        }

        public List<LogData> GetLogData(ObjectId id)
        {
            var log = CobbLogs.Find<Log>(l => l.Id == id).Single();
            return log.Logs;
        }
    }
}
