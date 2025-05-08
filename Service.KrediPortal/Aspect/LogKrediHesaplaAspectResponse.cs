using Common.KrediPortal.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.KrediPortal.Aspect
{
    [PSerializable]
    public class LogKrediHesaplaAspectResponse:OnMethodBoundaryAspect
    {
        private IMongoCollection<BsonDocument> GetCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("basvurulogdb");
            return database.GetCollection<BsonDocument>("RequestResponseLogs");
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            try
            {
                var collection = GetCollection();

                if (args.ReturnValue is List<DTOKrediHesaplaResponse> response)
                {
                    var result = response;

                    var responseJson = JsonConvert.SerializeObject(result);
                    var log = new BsonDocument
                {
                    { "Tarih", DateTime.Now },
                    { "Tür", "Response" },
                    { "İşlem", args.Method.Name },
                    { "Data", responseJson },
                    {"Kullanıcı",Environment.MachineName }

                };

                    collection.InsertOneAsync(log);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loglama sırasında hata: " + ex.Message);
            }

        }
    }
}
