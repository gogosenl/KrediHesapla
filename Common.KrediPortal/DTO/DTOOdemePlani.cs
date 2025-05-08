using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOOdemePlani
    {
        public int Ay { get; set; }
        public decimal AylikOdeme { get; set; }
        public decimal KalanTutar { get; set; }
        public decimal OdenecekTutar { get; set; }
        public decimal OdenecekTutarSigortasiz { get; set; }
        public decimal AylikOdemeSigortasiz { get; set; }
        public decimal KalanTutarSigortasiz { get; set; }
    }
}
