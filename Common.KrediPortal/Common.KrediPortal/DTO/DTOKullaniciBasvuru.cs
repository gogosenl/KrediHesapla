using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KrediPortal.DTO
{
    public class DTOKullaniciBasvuru
    {
        public int Id { get; set; }
        public int KrediTurId {  get; set; }
        public int KrediVade { get; set; }
        public int KrediTutar { get; set; }
        public int BankaId { get; set; }
        public int Tc { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int TelNo { get; set; }
        public DateTime BasvuruTarih { get; set; }

    }
}
