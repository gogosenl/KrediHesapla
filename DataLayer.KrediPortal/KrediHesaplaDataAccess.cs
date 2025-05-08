using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DataLayer.KrediPortal
{
    public class KrediHesaplaDataAccess
    {
        private readonly IConfiguration _configuration;

        public KrediHesaplaDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetKrediDetay(int krediTurId)
        {
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("kredidb");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_KrediHesaplaGetir", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@krediturid", krediTurId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }
                }
            }
            return table;
        }
    }
}
