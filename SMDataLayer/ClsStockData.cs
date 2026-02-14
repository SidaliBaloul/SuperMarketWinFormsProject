using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace SMDataLayer
{
    public static class ClsStockData
    {
        public static bool UpdateProductQuantityInStock(int productid, int usedquantity,SqlConnection connection, SqlTransaction tran)
        {
            //string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;


            SqlCommand command = new SqlCommand("SP_UpdateProductQuantityInStock", connection,tran);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", usedquantity);

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                return false;
            }
           

            return true;
        }

        public static DataTable GetProductsInStock()
        {
            DataTable dataTable = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "SELECT S.StockID, P.Name , S.QuantityAvailable , S.ExpDate FROM Stock S INNER JOIN Products P ON S.ProductID = P.ProductID";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
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

            return dataTable;
        }

        public static bool AddStock(int productid, int quantity, DateTime expdate)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddStock", connection);
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
    }
}
