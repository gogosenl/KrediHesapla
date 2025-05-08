using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOKrediHesaplaRequest
    {
        public int KrediTurId { get; set; }
        public decimal KrediTutar { get; set; }
        public int KrediVade { get; set; }
    }
}
