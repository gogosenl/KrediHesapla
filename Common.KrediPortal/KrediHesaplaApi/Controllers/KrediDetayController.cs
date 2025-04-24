using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.KrediPortal;

namespace KrediHesaplaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KrediDetayController : ControllerBase
    {
        private readonly IKrediDetay _krediDetay;

        public KrediDetayController(IKrediDetay krediDetay)
        {
            _krediDetay = krediDetay;
        }

        [HttpPost("EkleKrediDetay")]
        public IActionResult EkleKrediDetay([FromBody] DTOKrediDetay kredidetay)
        {
            try
            {
                DTOKrediDetay sonuc = _krediDetay.EkleKrediDetay(kredidetay);
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

        [HttpDelete("{Id}")]
        public IActionResult SilKrediDetay([FromBody] DTOKrediDetay kredidetay)
        {
            try
            {
                DTOKrediDetay sonuc = _krediDetay.SilKrediDetay(kredidetay);
                return Ok(new
                {
                    success = true,
                    message = "Kredi başarıyla silindi.",
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

        [HttpGet("kredidetaylari")]
        public ActionResult<List<DTOKrediDetay>> KrediDetaylari()
        {
            try
            {
                var sonuc = _krediDetay.KrediDetayList();
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }



    }
}
