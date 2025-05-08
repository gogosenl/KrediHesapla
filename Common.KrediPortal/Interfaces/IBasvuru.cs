using Common.KrediPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.Interfaces
{
    public interface IBasvuru
    {
        List<DTOKullaniciBasvuruList> ListKullaniciBasvuru();

        DTOKullaniciBasvuru EkleKullaniciBasvuru(DTOKullaniciBasvuru kullaniciBasvuru);

    }
}
