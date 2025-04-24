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
    public class KrediDetayDataAccess
    {
        string connectionString = @"Data Source=GOGO\SQLEXPRESS;Initial Catalog=kredidb;Integrated Security=True;Encrypt=False";

        public bool EkleKrediDetay(DTOKrediDetay kredidetay)
        {

            DateTime eklemetarih = Convert.ToDateTime(DateTime.Now);
            string ekleyen = Environment.MachineName;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("SP_KrediDetayEkle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@krediturid", kredidetay.KrediTurId);
                    cmd.Parameters.AddWithValue("@bankaid", kredidetay.BankaId);
                    cmd.Parameters.AddWithValue("@minvade", kredidetay.MinVade);
                    cmd.Parameters.AddWithValue("@maxvade", kredidetay.MaxVade);
                    cmd.Parameters.AddWithValue("@mintutar", kredidetay.MinTutar);
                    cmd.Parameters.AddWithValue("@maxtutar", kredidetay.MaxTutar);
                    cmd.Parameters.AddWithValue("@faizorani", kredidetay.FaizOrani);
                    cmd.Parameters.AddWithValue("@eklemetarih", eklemetarih);
                    cmd.Parameters.AddWithValue("@ekleyen", ekleyen);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool SilKrediDetay(string Id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_KrediDetaySil", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", Id);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }


        public List<DTOKrediDetay> KrediDetaylari()
        {
            var list = new List<DTOKrediDetay>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_KrediDetayList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new DTOKrediDetay
                            {
 
                                BankaAdi = dr["bankaadi"].ToString(),
                                BankaLink = dr["bankalink"].ToString(),
                                KrediTur = dr["kreditur"].ToString(),
                                MinVade = Convert.ToInt32(dr["minvade"]),
                                MaxVade = Convert.ToInt32(dr["maxvade"]),
                                MinTutar = Convert.ToInt32(dr["mintutar"]),
                                MaxTutar = Convert.ToInt32(dr["maxtutar"]),
                                FaizOrani = Convert.ToInt32(dr["faizorani"]),
                                EklemeTarih = Convert.ToDateTime(dr["eklemetarih"]),
                                Ekleyen = dr["ekleyen"].ToString(),
                            });
                        }
                    }
                }
            }

            return list;
        }



    }
}
