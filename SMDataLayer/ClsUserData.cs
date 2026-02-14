using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;
using System.Data;

namespace SMDataLayer
{
    public static class ClsUserData
    {
        
        public static bool GetUserByUserNameAndPasswordData(ref int userid,string username,string password)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_GetUserByUserNameAndPassword", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Password", password);

            SqlParameter outpuu = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            {
                outpuu.Direction  = System.Data.ParameterDirection.Output;
            };
            command.Parameters.Add(outpuu);

            connection.Open();
            command.ExecuteNonQuery();

            if (command.Parameters["@UserID"].Value != DBNull.Value)
            {
                userid = (int)command.Parameters["@UserID"].Value;
                return true;
            }

            connection.Close();

            return false;

        }

        public static bool IsPasswordCorrect(int userid,string password)
        {
            bool isfound = false;
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_IsPasswordCorrect", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserID", userid);
            command.Parameters.AddWithValue("@HashedPassword", password);

            connection.Open();

            object result = command.ExecuteScalar();

            if (result != DBNull.Value)
            {
                isfound = Convert.ToBoolean(result);
            }

            return isfound;
        }

        public static void UpdateUserPasswordData(int userid, string password)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionstring);

            SqlCommand command = new SqlCommand("SP_UpdateUserPassword", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserID", userid);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();

            object hh = command.ExecuteNonQuery();

            connection.Close();

        }


    }
}
