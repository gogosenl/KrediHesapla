using Common.KrediPortal.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.KrediPortal
{
    public class KullaniciBasvuruDataAccess
    {


        string connectionString = @"Data Source=GOGO\SQLEXPRESS;Initial Catalog=kredidb;Integrated Security=True;Encrypt=False";

        public bool EkleKullaniciBasvuru(DTOKullaniciBasvuru  kullaniciBasvuru)
        {

            DateTime basvurutarih = Convert.ToDateTime(DateTime.Now);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("SP_KullaniciBasvuru", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@krediturid", kullaniciBasvuru.KrediTurId);
                    cmd.Parameters.AddWithValue("@kredivade", kullaniciBasvuru.KrediVade);
                    cmd.Parameters.AddWithValue("@kreditutar", kullaniciBasvuru.KrediTutar);
                    cmd.Parameters.AddWithValue("@bankaid", kullaniciBasvuru.BankaId);
                    cmd.Parameters.AddWithValue("@tc", kullaniciBasvuru.Tc);
                    cmd.Parameters.AddWithValue("@ad", kullaniciBasvuru.Ad);
                    cmd.Parameters.AddWithValue("@soyad", kullaniciBasvuru.Soyad);
                    cmd.Parameters.AddWithValue("@telno", kullaniciBasvuru.TelNo);
                    cmd.Parameters.AddWithValue("@basvurutarih", basvurutarih);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
