using Common.KrediPortal.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.KrediPortal;

namespace KrediHesaplaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankaController : ControllerBase
    {
        [HttpPost("EkleBanka")]
        public IActionResult EkleBanka([FromBody] DTOBanka banka)
        {
            try
            {
                IBank bankaService = new BankaService();

                DTOBanka sonuc = bankaService.EkleBanka(banka); 
                return Ok(new
                {
                    success = true,
                    message = "Banka başarıyla eklendi.",
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
                // Genel hata yakalama
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
