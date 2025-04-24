using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Service.KrediPortal
{
    public class KrediHesaplaService 
        {
            private readonly KrediHesaplaDataAccess _dataAccess;

        public KrediHesaplaService(KrediHesaplaDataAccess dataAccess)
            {
                _dataAccess = dataAccess;
            }
        
        public async Task<List<DTOKrediHesaplaResponse>> Hesapla(DTOKrediHesaplaRequest request)
            {
 

                var table = _dataAccess.GetKrediDetay(request.KrediTurId);
                var sonuc = new List<DTOKrediHesaplaResponse>();

                foreach (DataRow row in table.Rows)
                {
                    decimal faizOrani = Convert.ToDecimal(row["faizorani"]);
                    decimal aylikOdeme = request.KrediTutar *
                        (faizOrani * (decimal)Math.Pow(1 + (double)faizOrani, request.KrediVade)) /
                        ((decimal)Math.Pow(1 + (double)faizOrani, request.KrediVade) - 1);

                    decimal aylikOdemeSigortasiz = aylikOdeme * 109 / 100;
                    decimal odenecekTutar = aylikOdeme * request.KrediVade;
                    decimal odenecekTutarSigortasiz = aylikOdemeSigortasiz * request.KrediVade;

                    decimal kalanTutar = odenecekTutar;
                    decimal kalanTutarSigortasiz = odenecekTutarSigortasiz;

                    var odemePlani = new List<DTOOdemePlani>();

                    for (int ay = 1; ay <= request.KrediVade; ay++)
                    {
                        if (ay == request.KrediVade)
                        {
                            aylikOdeme = kalanTutar;
                            aylikOdemeSigortasiz = kalanTutarSigortasiz;
                        }

                        kalanTutar -= aylikOdeme;
                        kalanTutarSigortasiz -= aylikOdemeSigortasiz;

                        odemePlani.Add(new DTOOdemePlani
                        {
                            Ay = ay,
                            AylikOdeme = aylikOdeme,
                            AylikOdemeSigortasiz = aylikOdemeSigortasiz,
                            OdenecekTutar = odenecekTutar,
                            OdenecekTutarSigortasiz = odenecekTutarSigortasiz,
                            KalanTutar = kalanTutar,
                            KalanTutarSigortasiz = kalanTutarSigortasiz
                        });
                    }

                    int minTutar = Convert.ToInt32(row["mintutar"]);
                    int maxTutar = Convert.ToInt32(row["maxtutar"]);
                    int minVade = Convert.ToInt32(row["minvade"]);
                    int maxVade = Convert.ToInt32(row["maxvade"]);
                    int bankaId = Convert.ToInt32(row["bankaid"]);
                    int krediTurId = Convert.ToInt32(row["krediturid"]);




                if (request.KrediTutar >= minTutar && request.KrediTutar <= maxTutar &&
                        request.KrediVade >= minVade && request.KrediVade <= maxVade)
                    {
                        sonuc.Add(new DTOKrediHesaplaResponse
                        {
                            KrediTur = row["kreditur"].ToString(),
                            BankaAdi = row["bankaadi"].ToString(),
                            BankaLink = row["bankalink"].ToString(),
                            KrediTutar = request.KrediTutar,
                            KrediVade = request.KrediVade,
                            FaizOrani = faizOrani,
                            AylikOdeme = aylikOdeme,
                            AylikOdemeSigortasiz = aylikOdemeSigortasiz,
                            OdenecekTutar = odenecekTutar,
                            OdenecekTutarSigortasiz = odenecekTutarSigortasiz,
                            KalanTutarSigortasiz = kalanTutarSigortasiz,
                            MinVade = minVade,
                            MaxVade = maxVade,
                            OdemePlani = odemePlani,
                            BankaId = bankaId,
                            KrediTurId = krediTurId,
                        });
                    }
                }


                if (!sonuc.Any())
                {
                    sonuc.Add(new DTOKrediHesaplaResponse
                    {
                        BankaAdi = "Girilen Kriterlere Uygun Banka bulunamadı",
                        FaizOrani = 0,
                        KrediTutar = request.KrediTutar,
                        KrediVade = request.KrediVade,
                        AylikOdeme = 0,
                        AylikOdemeSigortasiz = 0,
                        OdenecekTutar = request.KrediTutar,
                        OdenecekTutarSigortasiz = request.KrediTutar,
                        OdemePlani = new List<DTOOdemePlani>()
                    });
                }
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

     

            return sonuc;
            }

  

    }

    }

