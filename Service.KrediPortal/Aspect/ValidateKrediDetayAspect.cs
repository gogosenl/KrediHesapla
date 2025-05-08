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
    public class ValidateKrediDetayAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args.Arguments.Count == 0 || args.Arguments[0] == null)
                throw new ArgumentException("Başvuru nesnesi null olamaz.");

            var krediDetay = args.Arguments[0] as DTOKrediDetay;

            if (string.IsNullOrWhiteSpace(krediDetay.BankaAdi))
                throw new ArgumentException("bankaadi boş olamazaspect .");

            if (string.IsNullOrWhiteSpace(krediDetay.BankaLink))
                throw new ArgumentException("bankalink boş olamazaspect.");

            if (krediDetay.MinTutar <= 0)
                throw new ArgumentException("min tutarı 0 olamazaspect.");

            if (krediDetay.MaxTutar <= 0)
                throw new ArgumentException("max tutarı 0 olamazaspect.");
        }


    

    }

}
