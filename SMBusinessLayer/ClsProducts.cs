using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMBusinessLayer
{
    public class ClsProducts
    {
        public int ProductID { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }

        [RangeCustom(0.001 ,10000, ErrorMessage = "Price Must Be Between 0.001$ And 100000$ ")]
        public double Price { get; set; }


        ClsProducts(int productid, string barcode, string name, double price)
        {
            this.ProductID = productid;
            this.BarCode = barcode;
            this.Name = name;
            this.Price = price;

        }

        public static DataTable GetProductsList()
        {
            return ClsProductsData.GetProductsList();
        }

        public static DataTable GetProductsListForAdmin()
        {
            return ClsProductsData.GetProductsListForAdmin();
        }

        public static bool UpdateProductInfos(int productID, string barcode, double price)
        {
            return ClsProductsData.UpdateProductInfo(productID, barcode.ToString(), price);
        }

        public static bool RemoveProduct(int productid)
        {
            return ClsProductsData.RemoveProduct(productid);
        }

        public static bool AddProduct(string name, string barcode, double price)
        {
            return ClsProductsData.AddProduct(name, barcode.ToString(), price);
        }

        public static bool IsBarCodeAlreadyExists(string barcode)
        {
            return ClsProductsData.IsBarCodeAlreadyExists(barcode);
        }


        public static ClsProducts Find(int productid)
        {
            string barcode = "";
            string name = "";
            double price = 0.0;

            if (ClsProductsData.FindProduct(productid, ref barcode, ref name, ref price))
            {
                return new ClsProducts(productid, barcode, name, price);
            }
            else
                return null;
        }

        public static int FindByName(string name)
        {
            int productid = 0;

            if (ClsProductsData.FindByName(name, ref productid))
                return productid;

            return -1;
        }

        public static int GetProductQuantityAvailable(int productid)
        {
            return ClsProductsData.GetProductQuantityAvailable(productid);
        }

        public static double GetPriceOfAProduct(int productid)
        {
            return ClsProductsData.GetProductPrice(productid);
        }


    }
}
