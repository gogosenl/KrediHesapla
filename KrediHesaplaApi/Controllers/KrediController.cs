using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrediHesaplaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KrediController : ControllerBase
    {
        private readonly IKrediService _krediService;

        public KrediController(IKrediService krediService)
        {
            _krediService = krediService;
        }

        [HttpPost("EkleKrediTur")]
        public IActionResult EkleKrediTur([FromBody] DTOKrediTur kreditur)
        {
            try
            {
                DTOKrediTur sonuc = _krediService.EkleKrediTur(kreditur);
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
        public IActionResult SilKrediTur([FromBody] DTOKrediTur kreditur)
        {
            try
            {
                DTOKrediTur sonuc = _krediService.SilKrediTur(kreditur);
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

        [HttpGet("kreditur_getir")]
        public ActionResult<List<DTOKrediTurResponse>> GetKrediTurById(int id)
        {
            try
            {
                var sonuc = _krediService.KrediTurListVeVadeHesaplaById(id);
                if (sonuc == null || !sonuc.Any())
                {
                    return NotFound("Belirtilen ID için kredi türü bulunamadı.");
                }
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("krediturleri")]
        public ActionResult<List<DTOKrediTur>> KrediTurleri()
        {
            try
            {
                var sonuc = _krediService.KrediTurleri();
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}
