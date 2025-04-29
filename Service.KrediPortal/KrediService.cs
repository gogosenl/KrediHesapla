using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace Service.KrediPortal
{
    public class KrediService : IKrediService
    {

        private readonly IDatabase _redisDb;
        private readonly KrediDataAccess _dbKrediAccess;

        public KrediService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost"); 
            _redisDb = redis.GetDatabase();
            _dbKrediAccess = new KrediDataAccess();
        }





        public DTOKrediTur EkleKrediTur(DTOKrediTur kreditur)
        {
            #region Ekleme Kuralları

            if (string.IsNullOrWhiteSpace(kreditur.KrediTur))
            {
                throw new ArgumentException("Boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(kreditur.KrediMinVade)))
            {
                throw new ArgumentException("MinVade boş olamaz.");
            }
            #endregion

            #region Database İşlemleri

            KrediDataAccess dbKrediAccess = new KrediDataAccess();
            bool eklendi = dbKrediAccess.EkleKrediTur(kreditur);

            if (!eklendi)
            {
                throw new Exception("Banka ekleme işlemi başarısız oldu.");
            }
            #endregion
            return kreditur;
        }

        public DTOKrediTur SilKrediTur(DTOKrediTur kreditur)
        {
            #region Silme Kuralları

            if (string.IsNullOrWhiteSpace(Convert.ToString(kreditur.Id)))
            {
                throw new ArgumentException("Boş olamaz.");
            }
            #endregion

            #region Database İşlemleri

            KrediDataAccess dbKrediAccess = new KrediDataAccess();
            bool silindi = dbKrediAccess.SilKrediTur(Convert.ToString(kreditur.Id));

            if (!silindi)
            {
                throw new Exception("Banka silme işlemi başarısız oldu.");
            }
            #endregion
            return kreditur;
        }

        public List<DTOKrediTur> KrediTurleri()
        {
            KrediDataAccess dbKrediAccess = new KrediDataAccess();
            return dbKrediAccess.KrediTurleri();
        }


        public List<DTOKrediTurResponse> KrediTurListVeVadeHesaplaById(int id)
        {
            string cacheKey = $"KrediTurVeVade:{id}";
            var cachedData = _redisDb.StringGet(cacheKey);

            if (cachedData.HasValue)
            {
                return JsonConvert.DeserializeObject<List<DTOKrediTurResponse>>(cachedData);
            }

            var sonuc = new List<DTOKrediTurResponse>();

            var krediTurleri = _dbKrediAccess.KrediTurList(id);
            var secilenTur = krediTurleri.FirstOrDefault(k => k.Id == id);

            if (secilenTur == null)
                return sonuc;

            for (int vade = secilenTur.KrediMinVade; vade <= secilenTur.KrediMaxVade;)
            {
                sonuc.Add(new DTOKrediTurResponse
                {
                    Id = secilenTur.Id,
                    KrediVade = vade,
                });

                if (vade < 12)
                    vade += 3;
                else
                    vade += 12;
            }

            var serializedData = JsonConvert.SerializeObject(sonuc);
            _redisDb.StringSet(cacheKey, serializedData, TimeSpan.FromHours(12));

            return sonuc;
        }

        public object KrediTurleri(int krediTurId)
        {

            throw new NotImplementedException();
        }
    }
}