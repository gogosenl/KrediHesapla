using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.KrediPortal
{
    public class KullaniciBasvuruService :IBasvuru
    {

        public DTOKullaniciBasvuru EkleKullaniciBasvuru(DTOKullaniciBasvuru kullaniciBasvuru)
        {
            #region Ekleme Kuralları

            if (string.IsNullOrWhiteSpace(kullaniciBasvuru.Ad))
            {
                throw new ArgumentException("Ekleyen Boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(kullaniciBasvuru.KrediTutar)))
            {
                throw new ArgumentException("MinVade boş olamaz.");
            }
            #endregion

            #region Database İşlemleri

            KullaniciBasvuruDataAccess dbKullaniciAccess = new KullaniciBasvuruDataAccess();
            bool eklendi = dbKullaniciAccess.EkleKullaniciBasvuru(kullaniciBasvuru);

            if (!eklendi)
            {
                throw new Exception("Başvuru işlemi başarısız oldu.");
            }
            #endregion
            return kullaniciBasvuru;
        }




    }
}
