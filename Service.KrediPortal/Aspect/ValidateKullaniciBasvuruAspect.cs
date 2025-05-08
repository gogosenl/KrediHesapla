using Common.KrediPortal.DTO;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.KrediPortal.Aspect
{
    [PSerializable]
    public class ValidateKullaniciBasvuruAspect : OnMethodBoundaryAspect
    {

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args.Arguments.Count == 0 || args.Arguments[0] == null)
                throw new ArgumentException("Başvuru nesnesi null olamaz.");

            var basvuru = args.Arguments[0] as DTOKullaniciBasvuru;

            if (string.IsNullOrWhiteSpace(basvuru.Ad))
                throw new ArgumentException("Ad boş olamazaspect .");

            if (string.IsNullOrWhiteSpace(basvuru.Soyad))
                throw new ArgumentException("Soyad boş olamazaspect.");

            if (string.IsNullOrWhiteSpace(basvuru.Tc))
                throw new ArgumentException("TC geçersiz.");

            if (string.IsNullOrWhiteSpace(basvuru.TelNo))
                throw new ArgumentException("Tel No geçersiz.");

            if (basvuru.KrediTutar <= 0)
                throw new ArgumentException("Kredi tutarı 0 olamazaspect.");

        }
    }
}
