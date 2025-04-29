using Common.KrediPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.KrediPortal;


namespace KrediHesaplaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KrediHesaplaController : ControllerBase
    {
        private readonly KrediHesaplaService _service;

        public KrediHesaplaController(KrediHesaplaService service)
        {
            _service = service;
        }

        [HttpPost("tutar_hesapla")]
        public async Task<IActionResult> TutarHesapla([FromBody] DTOKrediHesaplaRequest krediRequest)
        {
            var result = await _service.Hesapla(krediRequest);
            return Ok(result);
        }
    }
}
