using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    public class ClsCart
    {
        public int No { get; set; }
        public int ProductID { get; set; }

        [RangeCustom(1 , int.MaxValue, ErrorMessage = "Quantity Must Be Atleast 1")]
        public int Quantity { get; set; }
        public double Total { get; set; }

        public delegate void DataBackDelegate(double total);
        public static event DataBackDelegate databack;

        public ClsCart()
        {
            No = 0;
            ProductID = 0;
            Quantity = 0;
            Total = 0;
        }

        public static bool IsProductExistsInCart(int productid)
        {
            return ClsCartData.IsProductExistsInCart(productid);
        }

        public bool AddProductToCart(int productID, int quantity, double price,string UpdateQuantityMode = "ADD")
        {
            ProductID = productID;
            Quantity = quantity;
            Total = price * quantity;


            
            if(!ClsCart.IsProductExistsInCart(productID))
               No = ClsCartData.AddProductToCartData(ProductID,Quantity,Total);
            else
                No = ClsCartData.UpdateProductInCart(productID,Quantity,Total,UpdateQuantityMode);

            if (No != -1 && UpdateQuantityMode == "ADD")
                databack?.Invoke(Total);

            else if(No != -1 && UpdateQuantityMode == "REMOVE")
                    databack?.Invoke(Total * -1);


            return (No != -1);
        }

        public static DataTable GetCartList()
        {
            return ClsCartData.GetCartListData();
        }

        public static double GetCartProductsTotal()
        {
            return ClsCartData.GetCartProductsTotalData();
        }

        public static int GetProductQuantityInCart(int productid)
        {
            return ClsCartData.GetProductQuantityInCart(productid);
        }

        public static bool DeleteProductFromCart(int productid,double total)
        {
           if(ClsCartData.DeleteProductFromCartData(productid))
           {
                databack?.Invoke(total * -1);
                return true;
           }

           return false;
        }

        public static bool ClearCart(SqlConnection connection = null, SqlTransaction tran = null)
        {

            return ClsCartData.ClearCartData(connection, tran);
        }
        public static bool ClearCart()
        {

            return ClsCartData.ClearCartData();
        }

        public static bool IsCartEmpty()
        {
            return (ClsCartData.GetCartListData().Rows.Count == 0);
        }

    }
}
