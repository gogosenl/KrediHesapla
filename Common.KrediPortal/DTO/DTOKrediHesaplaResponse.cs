using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOKrediHesaplaResponse
    {
        public string KrediTur { get; set; }
        public string BankaAdi { get; set; }
        public string BankaLink { get; set; }
        public int KrediVade { get; set; }
        public decimal KrediTutar { get; set; }
        public decimal AylikOdeme { get; set; }
        public decimal AylikOdemeSigortasiz { get; set; }
        public decimal OdenecekTutar { get; set; }
        public decimal OdenecekTutarSigortasiz { get; set; }
        public decimal KalanTutarSigortasiz { get; set; }
        public decimal FaizOrani { get; set; }
        public int MinVade { get; set; }
        public int MaxVade { get; set; }
        public int BankaId { get; set; }
        public int KrediTurId { get; set; }
        public List<DTOOdemePlani> OdemePlani { get; set; }
    }
}
