using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOKullaniciBasvuruList
    {
        public int Id { get; set; }
        public string KrediTur { get; set; }
        public int KrediVade { get; set; }
        public int KrediTutar { get; set; }
        public string BankaAdi { get; set; }
        public string Tc { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TelNo { get; set; }
        public DateTime BasvuruTarih { get; set; }
    }
}
