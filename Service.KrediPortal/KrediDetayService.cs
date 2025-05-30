﻿using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.KrediPortal
{
    public class KrediDetayService:IKrediDetay
    {

        public DTOKrediDetay EkleKrediDetay(DTOKrediDetay kredidetay)
        {
            #region Ekleme Kuralları

            if (string.IsNullOrWhiteSpace(kredidetay.Ekleyen))
            {
                throw new ArgumentException("Ekleyen Boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(kredidetay.MinVade)))
            {
                throw new ArgumentException("MinVade boş olamaz.");
            }
            #endregion

            #region Database İşlemleri

            KrediDetayDataAccess dbKrediAccess = new KrediDetayDataAccess();
            bool eklendi = dbKrediAccess.EkleKrediDetay(kredidetay);

            if (!eklendi)
            {
                throw new Exception("Banka ekleme işlemi başarısız oldu.");
            }
            #endregion
            return kredidetay;
        }

        public List<DTOKrediDetay> KrediDetayList()
        {
            KrediDetayDataAccess dbKrediAccess = new KrediDetayDataAccess();
            return dbKrediAccess.KrediDetaylari();
        }


        public DTOKrediDetay SilKrediDetay(DTOKrediDetay kredidetay)
        {
            #region Silme Kuralları

            if (string.IsNullOrWhiteSpace(Convert.ToString(kredidetay.Id)))
            {
                throw new ArgumentException("Boş olamaz.");
            }
            #endregion

            #region Database İşlemleri

            KrediDetayDataAccess dbKrediAccess = new KrediDetayDataAccess();
            bool silindi = dbKrediAccess.SilKrediDetay(Convert.ToString(kredidetay.Id));

            if (!silindi)
            {
                throw new Exception("Kredi Detay silme işlemi başarısız oldu.");
            }
            #endregion
            return kredidetay;
        }
    }
}
