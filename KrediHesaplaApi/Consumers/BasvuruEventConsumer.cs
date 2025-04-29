using Common.KrediPortal.DTO;
using DotNetCore.CAP;

namespace KrediHesaplaApi.Consumers
{
    public class BasvuruEventConsumer : ICapSubscribe
    {
        [CapSubscribe("kullanici.basvuru.olusturuldu")]
        public void Consumer(DTOKullaniciBasvuru basvuru)
        {
            Console.WriteLine($"CAP Consumer çalıştı! Kullanıcı: {basvuru.Ad} {basvuru.Soyad}");
            // Log, başka servis çağrısı vs. burada yapılabilir
        }
    }
}
