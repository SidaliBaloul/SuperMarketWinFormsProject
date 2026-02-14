using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMDataLayer
{
    public static class ClsProductsData
    {
        public static DataTable GetProductsList()
        {
            DataTable dataTable = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "SELECT * FROM dbo.GetProductsListForSearch()";

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.Text;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dataTable.Load(reader);
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

            return dataTable;
        }

        public static bool FindProduct(int productid,ref string barcode,ref string name,ref double price)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection( connectionstring);

            SqlCommand command = new SqlCommand("SP_FindProduct", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                barcode = (string)reader["BarCode"];
                name = (string)reader["Name"];
                price = Convert.ToDouble(reader["Price"]);

                isfound = true;
            }
            else
                isfound = false;

            reader.Close();
            connection.Close();

            return isfound;
        }

        public static bool FindByName(string name, ref int productid)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_FindProductByName", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", name);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                productid = (int)reader["ProductID"];

                isfound = true;
            }
            else
                isfound = false;

            reader.Close();
            connection.Close();

            return isfound;
        }

        public static int GetProductQuantityAvailable(int productid)
        {
            int quantity = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "SELECT TOP 1 QuantityAvailable FROM Stock WHERE ProductID = @productID AND QuantityAvailable > 0 ORDER BY ExpDate ASC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productID", productid);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insted))
                {
                    quantity = insted;
                }
            }
            catch(Exception ex) 
            { 

            }
            finally
            {
                connection.Close();
            }

            return quantity;
        }

        public static DataTable GetProductsListForAdmin()
        {
            DataTable dataTable = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetProductsAdmin", connection);
            command.CommandType = CommandType.StoredProcedure;

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

        public static bool UpdateProductInfo(int productid,string barcode, double price)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_UpdateProductInfos", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@BarCode", barcode);
            command.Parameters.AddWithValue("@Price", price);

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

        public static bool RemoveProduct(int productid)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_RemoveProduct", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);


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

        public static bool AddProduct(string name, string barcode, double price)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddProductToProducts", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@BarCode", barcode);
            command.Parameters.AddWithValue("@Price", price);

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

        public static bool IsBarCodeAlreadyExists(string barcode)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SELECT dbo.SF_IsBarCodeExists(@BarCode);", connection);
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@BarCode", barcode);

            connection.Open();
            object mm = Convert.ToBoolean(command.ExecuteScalar());

            if (mm != null && bool.TryParse(mm.ToString(), out bool insted))
            {
                isfound = insted;
            }

            connection.Close();

            return isfound;
        }

        public static double GetProductPrice(int productid)
        {
            double price = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "SELECT Price FROM Products WHERE ProductID = @productID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productID", productid);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && double.TryParse(result.ToString(), out double insted))
                {
                    price = insted;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return price;
        }
    }

            
            
}
