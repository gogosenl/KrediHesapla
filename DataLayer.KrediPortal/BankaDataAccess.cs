namespace DataLayer.KrediPortal
{
    using System.Data;
    using System.Data.SqlClient;

    public class BankaDataAccess
    {
        string connectionString = @"Data Source=GOGO\SQLEXPRESS;Initial Catalog=kredidb;Integrated Security=True;Encrypt=False";

        public bool EkleBanka(DTOBanka banka)
        {
            DateTime eklemetarih = Convert.ToDateTime(DateTime.Now);
            string ekleyen = Environment.MachineName;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("SP_BankaEkle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bankaadi", banka.BankaAdi);
                    cmd.Parameters.AddWithValue("@bankalink", banka.BankaLinki);
                    cmd.Parameters.AddWithValue("@ekleyen", ekleyen);
                    cmd.Parameters.AddWithValue("@eklemetarih", eklemetarih);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        
    }
}
