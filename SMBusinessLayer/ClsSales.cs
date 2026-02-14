using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SMBusinessLayer
{
    public class ClsSales
    {

        public static DataTable GetSalesList()
        {
            return ClsSalesData.GetSalesList();
        }

        public static DataTable GetSalesDetailsList(int saleid)
        {
            return ClsSalesData.GetSaleDetails(saleid);
        }

        public static double GetTotalSales()
        {
            return ClsSalesData.GetTotalSales();
        }

        public static Nullable<int> AddSaleRecord(double amount, int userid, SqlConnection connection, SqlTransaction tran)
        {
            Nullable<int> ID = ClsSalesData.AddSaleRecordData(amount, userid,connection,tran);

            return ID;
        }

        public static bool AddSaleDetailsData(int saleid, int productid, int quantity, SqlConnection connection, SqlTransaction tran)
        {
            return ClsSalesData.AddSaleDetailsData(saleid, productid, quantity, connection, tran);
        }

        public static DataTable FilterDataByDate(int option)
        {
            return ClsSalesData.GetSalesFilter(option);
        }

        public static void CompleteSale(double _Total)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["SuperMarketConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    Nullable<int> ID = ClsSales.AddSaleRecord(_Total, 2,connection,tran);

                    if (ID == null)
                    {
                        throw new Exception("Failed To Add Sale Record !");
                    }

                    DataTable dt = ClsCart.GetCartList();

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!ClsSales.AddSaleDetailsData((int)ID, (int)row[1], (int)row[2],connection,tran))
                        {
                            throw new Exception("Failed To Add Sale Detailes !");
                        }

                        if(!ClsStock.UpdateProductQuantityinStock((int)row[1], (int)row[2],connection,tran))
                            throw new Exception("Failed To Update Quantity In Stock !");
                    }

                    
                    ClsCart.ClearCart(connection, tran);
                    tran.Commit();
                    connection.Close();
                }
                catch (Exception ex) 
                { 
                    tran.Rollback();
                    throw;
                }
            }

        }

    }
}
