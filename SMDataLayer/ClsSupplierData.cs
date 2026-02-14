using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMDataLayer
{
    public static class ClsSupplierData
    {
        public static DataTable GetSuppliersList()
        {
            DataTable dataTable = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetSuppliers", connection);
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

        public static int AddSupplier(string name, decimal phone , string email)
        {
            int ID = -1;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_AddSupplier", connection);
            command.CommandType = CommandType.StoredProcedure;

            
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);

            SqlParameter outpuu = new SqlParameter("@SupplierID", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@SupplierID"].Value != DBNull.Value)
            {
                ID = (int)command.Parameters["@SupplierID"].Value;
            }

            connection.Close();

            return ID;
        }

        public static bool Find(int supplierid, ref string name, ref decimal phone, ref string email)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_FindSupplier", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SupplierID", supplierid);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                name = (string)reader["Name"];
                phone = (decimal)reader["Phone"];
                email = (string)reader["Email"];

                isfound = true;
            }
            else
                isfound = false;

            reader.Close();
            connection.Close();

            return isfound;
        }

        public static bool FindByname(string suppliername, ref int supplierid)
        {
            bool isfound = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_FindSupplierByName", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", suppliername);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                supplierid = (int)reader["SupplierID"];

                isfound = true;
            }
            else
                isfound = false;

            reader.Close();
            connection.Close();

            return isfound;
        }

        public static bool UpdateSupplier(int supplierid, decimal phone, string email)
        {
            int RowsAffected = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_UpdateSupplier", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@SupplierID", supplierid);

            SqlParameter outpuu = new SqlParameter("@RowsAffected", System.Data.SqlDbType.Int);
            {
                outpuu.Direction = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@RowsAffected"].Value != DBNull.Value)
            {
                RowsAffected = (int)command.Parameters["@RowsAffected"].Value;
            }

            connection.Close();

            return (RowsAffected > 0);
        }

        public static bool RemoveSupplier(int supplierid)
        {
            int RowsAffected = 0;

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_RemoveSupplier", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@SupplierID", supplierid);


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
    }
}
