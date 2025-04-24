using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace KrediHesaplaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciBasvuruController : ControllerBase
    {
        private readonly IBasvuru _kullanicibasvuru;
        private readonly ICapPublisher _capPublisher;

        public KullaniciBasvuruController(IBasvuru kullanicibasvuru)
        {
            _kullanicibasvuru = kullanicibasvuru;
        }

        [HttpPost("EkleKullaniciBasvuru")]
        public async Task<IActionResult> EkleKullaniciBasvuru([FromBody] DTOKullaniciBasvuru kullaniciBasvuru)
        {
            if (kullaniciBasvuru == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Gönderilen veri boş. Lütfen formu doldurunuz."
                });
            }

            try
            {
                var sonuc = _kullanicibasvuru.EkleKullaniciBasvuru(kullaniciBasvuru);



                return Ok(new
                {
                    success = true,
                    message = "Kredi başarıyla eklendi.",
                    data = sonuc
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Sunucu hatası oluştu.",
                    detail = ex.Message
                });
            }
        }
    }
}
