using Common.KrediPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.Interfaces
{
    public interface IKrediService
    {
        List<DTOKrediTurResponse> KrediTurListVeVadeHesaplaById(int id);
        DTOKrediTur EkleKrediTur(DTOKrediTur kreditur);
        DTOKrediTur SilKrediTur(DTOKrediTur kreditur);
        List<DTOKrediTur> KrediTurleri();

    }


}
