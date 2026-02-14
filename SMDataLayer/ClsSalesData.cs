using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SMDataLayer
{
    public static class ClsSalesData
    {
        public static DataTable GetSalesList()
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetSales", connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetSaleDetails(int saleid)
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetSaleDetails", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SaleID", saleid);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetSalesFilter(int option)
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_FilterSalesWithDate", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Option", option);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static Nullable<int> AddSaleRecordData(double amount, int userid, SqlConnection connection, SqlTransaction tran)
        {
            Nullable<int> ID = null;

            //string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            //connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddSale", connection,tran);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@UserID", userid);
            command.Parameters.AddWithValue("@Date", DateTime.Now);

            SqlParameter outpuu = new SqlParameter("@SaleID", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);



            command.ExecuteNonQuery();

            if (command.Parameters["@SaleID"].Value != DBNull.Value)
            {
                ID = (int)command.Parameters["@SaleID"].Value;
            }
            else
                ID = null;

            return ID;
        }

        public static bool AddSaleDetailsData(int saleid, int productid, int quantity, SqlConnection connection, SqlTransaction tran)
        {
            //string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            //connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddSaleDetails", connection,tran);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SaleID", saleid);
            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", quantity);


            try
            {
                //connection.Open();

                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return false;
            }


            return true;
        }

        public static double GetTotalSales()
        {
            double amount = 0.0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SELECT dbo.GetTotalSales();", connection);
            command.CommandType = CommandType.Text;


            connection.Open();
            object mm = Convert.ToDouble(command.ExecuteScalar());

            if (mm != null && double.TryParse(mm.ToString(), out double insted))
            {
                amount = insted;
            }

            connection.Close();

            return amount;
        }


    }
}
