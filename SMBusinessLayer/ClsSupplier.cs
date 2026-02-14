using SMDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    public class ClsSupplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public decimal Phone { get; set; }
        public string Email { get; set; }

        public ClsSupplier()
        {
            SupplierID = 0;
        }

        private ClsSupplier(string name, decimal phone, string email)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        public static DataTable GetSuppliersList()
        {
            return ClsSupplierData.GetSuppliersList();
        }

        public bool AddSupplier()
        {
           SupplierID = ClsSupplierData.AddSupplier(this.Name, this.Phone, this.Email);

            return (SupplierID > 0);
        }

        public bool UpdateSupplier(int supplierid, decimal phone, string email)
        {
            return ClsSupplierData.UpdateSupplier(supplierid, phone, email);
        }

        public static ClsSupplier Find(int supplierid)
        {
            string name = "";
            decimal phone = 0;
            string email = "";

            if (ClsSupplierData.Find(supplierid, ref name, ref phone, ref email))
                return new ClsSupplier(name, phone, email);
            else
                return null;

        }

        public static int FindByName(string suppliername)
        {
            int supplierid = -1;

            if (ClsSupplierData.FindByname(suppliername, ref supplierid))
                return supplierid;

            return supplierid;
        }

        public static bool RemoveSupplier(int supplierid)
        {
            return ClsSupplierData.RemoveSupplier(supplierid);
        }


    }
}
