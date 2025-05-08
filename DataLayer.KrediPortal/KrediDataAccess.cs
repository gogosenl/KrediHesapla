using Common.KrediPortal.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer.KrediPortal
{
    public class KrediDataAccess
    {
        string connectionString = @"Data Source=GOGO\SQLEXPRESS;Initial Catalog=kredidb;Integrated Security=True;Encrypt=False";

        public bool EkleKrediTur(DTOKrediTur kreditur)
        {
            DateTime eklemetarih = Convert.ToDateTime(DateTime.Now);
            string ekleyen = Environment.MachineName;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("SP_KrediTurEkle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kreditur", kreditur.KrediTur);
                    cmd.Parameters.AddWithValue("@kredimaxvade", kreditur.KrediMaxVade);
                    cmd.Parameters.AddWithValue("@krediminvade", kreditur.KrediMinVade);
                    cmd.Parameters.AddWithValue("@ekleyen", ekleyen);
                    cmd.Parameters.AddWithValue("@eklemetarih", eklemetarih);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool SilKrediTur(string Id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_KrediTurSil", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@krediturid", Id);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public List<DTOKrediTur> KrediTurList(int krediTurId )
        {
            var list = new List<DTOKrediTur>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_KrediTurList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                  
                        cmd.Parameters.AddWithValue("@krediturid", krediTurId);
                    

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new DTOKrediTur
                            {
                                Id = Convert.ToInt32(dr["krediturid"]),
                                KrediMinVade = Convert.ToInt32(dr["krediminvade"]),
                                KrediMaxVade = Convert.ToInt32(dr["kredimaxvade"])
                            });
                        }
                    }
                }
            }

            return list;
        }


        public List<DTOKrediTur> KrediTurleri()
        {
            var list = new List<DTOKrediTur>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_Krediturleri", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new DTOKrediTur
                            {
                                Id = Convert.ToInt32(dr["krediturid"]),
                                KrediTur = dr["kreditur"].ToString(),
                                KrediMinVade = Convert.ToInt32(dr["krediminvade"]),
                                KrediMaxVade = Convert.ToInt32(dr["kredimaxvade"])
                            });
                        }
                    }
                }
            }

            return list;
        }






    }
}
