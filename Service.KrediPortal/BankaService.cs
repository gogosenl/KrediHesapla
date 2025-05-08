using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;

namespace Service.KrediPortal
{
    public class BankaService :IBank
    {
        public DTOBanka EkleBanka(DTOBanka banka)
        {
            #region Ekleme Kuralları

            // Geçerli bir link mi kontrolü
            if (string.IsNullOrWhiteSpace(banka.BankaLinki) || !banka.BankaLinki.StartsWith("https"))
            {
                throw new ArgumentException("Banka linki geçersiz. 'https' ile başlamalıdır.");
            }

            // Banka adı boş olmamalı
            if (string.IsNullOrWhiteSpace(banka.BankaAdi))
            {
                throw new ArgumentException("Banka adı boş olamaz.");
            }

            #endregion

            #region Database İşlemleri

            BankaDataAccess dbBankaAccess = new BankaDataAccess();
            bool eklendi = dbBankaAccess.EkleBanka(banka);

            if (!eklendi)
            {
                throw new Exception("Banka ekleme işlemi başarısız oldu.");
            }

            #endregion

            return banka;
        }



    }
}
