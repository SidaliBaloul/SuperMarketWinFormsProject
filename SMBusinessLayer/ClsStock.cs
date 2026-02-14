using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    public class ClsStock
    {
        public static bool UpdateProductQuantityinStock(int productID,int usedquantity, SqlConnection connection, SqlTransaction tran)
        {
             return ClsStockData.UpdateProductQuantityInStock(productID,usedquantity,connection,tran);
        }

        public static DataTable GetProductsInStock()
        {
            return ClsStockData.GetProductsInStock();
        }

        public static bool AddStock(int productid, int quantity, DateTime expdate)
        {
            return ClsStockData.AddStock(productid, quantity, expdate);
        }
    }
}
