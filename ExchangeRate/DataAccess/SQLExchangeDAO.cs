using ExchangeRate.Utility;
using System.Data;
using System.Data.SqlClient;

namespace ExchangeRate.DataAccess
{
    public class SQLExchangeDAO
    {
        string connectionString = ConnectionString.CName;
        public bool UpdateConversionFactor(string fromCurrency,string toCurrency,decimal updateExchangeRate)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPD_ExchangeRate", con);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromCurrency", fromCurrency);
                    cmd.Parameters.AddWithValue("@toCurrency", toCurrency);
                    cmd.Parameters.AddWithValue("@updateExchangeRate", updateExchangeRate);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected>0?true:false;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
