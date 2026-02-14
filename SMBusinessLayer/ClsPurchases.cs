using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    public class ClsPurchases
    {

        public static DataTable GetPurchasesList()
        {
            return ClsPurchasesData.GetPurchasesList();
        }

        public static DataTable GetPurchasesToStockIn()
        {
            return ClsPurchasesData.GetPurchasesToStockIn();
        }

        public static bool StockIn(int productid, int quantity, DateTime expdate)
        {
            return ClsPurchasesData.StockIn(productid, quantity, expdate);
        }

        public static bool UpdateStockedInStatus(int purchaseid)
        {
            return ClsPurchasesData.UpdateStockedInStatus(purchaseid);
        }

        public static bool AddPurchase(int productid, int quantity, double priceperunit, double total, int supplierid, DateTime purchasedate,DateTime expdate)
        {
            return ClsPurchasesData.AddPurchase(productid, quantity, priceperunit, total, supplierid, purchasedate,expdate);
        }


    }
}
