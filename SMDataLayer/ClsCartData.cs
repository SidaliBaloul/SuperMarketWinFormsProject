using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataLayer
{
    public static class ClsCartData
    {
         public static int AddProductToCartData(int productid,int quantity, double total)
         {
            int ID = -1;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddProductToCart", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Total", total);

            SqlParameter outpuu = new SqlParameter("@No", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@No"].Value != DBNull.Value)
            {
                ID = (int)command.Parameters["@No"].Value;
            }

            connection.Close();

            return ID;
        }

         public static DataTable GetCartListData()
         {
            DataTable dataTable = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetCartList", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

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

        public static double GetCartProductsTotalData()
        {
            double total = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_CalculateTotalCartProducts", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter outpuu = new SqlParameter("@Total", System.Data.SqlDbType.Money);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@Total"].Value != DBNull.Value)
            {
                total = Convert.ToDouble(command.Parameters["@Total"].Value);
            }

            connection.Close();

            return total;

        }

        public static int GetProductQuantityInCart(int productid)
        {
            int quantity = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = "SELECT * FROM dbo.GetProductQuantityInCart(@productid)";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productid", productid);

            command.CommandType = CommandType.Text;

            try
            {
                connection.Open();
                object mm = Convert.ToInt32(command.ExecuteScalar());

                if(mm != null && int.TryParse(mm.ToString(),out int insted))
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

        public static int UpdateProductInCart(int productid, int quantity, double total, string typeofoperation)
        {
            int RowsAffected = -1;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_UpdateProductInCart", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Total", total);
            command.Parameters.AddWithValue("@TypeOfOperation", typeofoperation);

            SqlParameter outpuu = new SqlParameter("@No", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();


             RowsAffected = (int)command.Parameters["@No"].Value;
            

            connection.Close();

            return RowsAffected;
        }

        public static bool IsProductExistsInCart(int productid)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SELECT dbo.SF_IsProductExistsInCart(@productid);", connection);
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@productid", productid);

            connection.Open();
            object mm = Convert.ToBoolean(command.ExecuteScalar());

            if (mm != null && bool.TryParse(mm.ToString(), out bool insted))
            {
                isfound = insted;
            }

            connection.Close();

            return isfound;
        }

        public static bool DeleteProductFromCartData(int productid)
        {
            int RowsAffected = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection( connectionstring);

            SqlCommand command = new SqlCommand("SP_DeleteProductFromCart", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductID", productid);


            SqlParameter outpuu = new SqlParameter("@RowsAffected", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            object result = command.ExecuteNonQuery();

            RowsAffected = (int)command.Parameters["@RowsAffected"].Value;

            connection.Close();

            return (RowsAffected > 0);
        }

        public static bool ClearCartData(SqlConnection connection, SqlTransaction tran)
        {
            int affectedrows = 0;
            

            SqlCommand command = new SqlCommand("SP_ClearCart", connection,tran);
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter output = new SqlParameter("@AffectedRows", System.Data.SqlDbType.Int);
            {
                output.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(output);

           
            command.ExecuteNonQuery();

            if (command.Parameters["@AffectedRows"].Value != DBNull.Value )
            {
                affectedrows = (int)command.Parameters["@AffectedRows"].Value;
            }

           

            return (affectedrows > 0);
        }

        public static bool ClearCartData()
        {
            int affectedrows = 0;
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_ClearCart", connection);
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter output = new SqlParameter("@AffectedRows", System.Data.SqlDbType.Int);
            {
                output.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(output);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@AffectedRows"].Value != DBNull.Value)
            {
                affectedrows = (int)command.Parameters["@AffectedRows"].Value;
            }

            connection.Close();

            return (affectedrows > 0);
        }

    }
}
