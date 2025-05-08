using Common.KrediPortal.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Configuration;
using System.Reflection.PortableExecutable;

[PSerializable]
public class LogKrediHesaplaAspect : OnMethodBoundaryAspect
{
    private IMongoCollection<BsonDocument> GetCollection()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("basvurulogdb");
        return database.GetCollection<BsonDocument>("RequestResponseLogs");
    }
    
    public override void OnEntry(MethodExecutionArgs args)
    {
        if (args.Arguments.Count > 0 && args.Arguments[0] is DTOKrediHesaplaRequest request)
        {
            var collection = GetCollection();

            var requestJson = JsonConvert.SerializeObject(request);
            var log = new BsonDocument
            {
                { "Tarih", DateTime.Now },
                { "Tür", "Request" },
                { "İşlem", args.Method.Name },
                { "Data", requestJson },
                {"Kullanıcı",Environment.MachineName }
            };

            collection.InsertOneAsync(log);
        }
    }

}
