using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMDataLayer
{
    public static class ClsPurchasesData
    {
        public static DataTable GetPurchasesList()
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_PurchasesList", connection);
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

        public static DataTable GetPurchasesToStockIn()
        {
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_PurchasesToStockIn", connection);
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

        public static bool StockIn(int productid, int quantity, DateTime expdate)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_StockIn", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@ExpDate", expdate);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public static bool AddPurchase(int productid, int quantity, double priceperunit, double total, int supplierid, DateTime purchasedate, DateTime expdate)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddPurchase", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@PricePerUnit", priceperunit);
            command.Parameters.AddWithValue("@Total", total);
            command.Parameters.AddWithValue("@SupplierID", supplierid);
            command.Parameters.AddWithValue("@PurchaseDate", purchasedate);
            command.Parameters.AddWithValue("@ExpDate", expdate);
            command.Parameters.AddWithValue("@StokedIn", 0);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public static bool UpdateStockedInStatus(int purchaseid)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "Update Purchases SET StokedIn = 1 WHERE PurchaseID = @PurchaseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PurchaseID", purchaseid);


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}
