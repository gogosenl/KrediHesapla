using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using Service.KrediPortal.Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.KrediPortal
{
    public class KrediDetayService:IKrediDetay
    {

        [ValidateKrediDetayAspect]
        public DTOKrediDetay EkleKrediDetay(DTOKrediDetay kredidetay)
        {
            #region Ekleme Kuralları


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
