using Common.KrediPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.Interfaces
{
    public interface IKrediDetay
    {
        List<DTOKrediDetay> KrediDetayList();
        DTOKrediDetay EkleKrediDetay(DTOKrediDetay kredidetay);
        DTOKrediDetay SilKrediDetay(DTOKrediDetay kredidetay);

    }
}
