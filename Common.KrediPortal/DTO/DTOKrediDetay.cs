using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOKrediDetay
    {
        public int Id { get; set; }
        public string KrediTur { get; set; }
        public string BankaAdi { get; set; }
        public string BankaLink { get; set; }
        public int KrediTurId { get; set; }
        public int BankaId { get; set; }
        public int MinVade { get; set; }
        public int MaxVade { get; set; }
        public int MinTutar { get; set; }
        public int MaxTutar { get; set; }
        public decimal FaizOrani { get; set; }
        public DateTime EklemeTarih { get; set; }
        public string Ekleyen { get; set; }


    }

}
