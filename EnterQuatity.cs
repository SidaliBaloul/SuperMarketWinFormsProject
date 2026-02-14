using SMBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class EnterQuatity : Form
    {
        bool DegitsOnly = true;
        int Quantity { get; set; }
        int ProductID { get; set; }
        double Price { get; set; }

        ClsCart cart;

        

        public EnterQuatity(int quantity,int productid,double price)
        {
            InitializeComponent();

            Quantity = quantity;
            ProductID = productid;
            Price = price;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DegitsOnly)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Error ! Quantity Cannot Be Blank ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int QuantityInCart = ClsCart.GetProductQuantityInCart(ProductID);
            int NetQuantityAfterCart = Quantity - QuantityInCart;

            if (int.TryParse(textBox1.Text.Trim(), out int quanitity))
            {
                if(quanitity > NetQuantityAfterCart)
                {
                    MessageBox.Show("Error ! Quantity Available Including Quantity In Cart (If Found): " + NetQuantityAfterCart.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } 

            var properity = typeof(ClsCart).GetProperty("Quantity");
            var rangeattr = properity.GetCustomAttribute<RangeCustomAttribute>();

            if (rangeattr != null)
            {
                if (quanitity < rangeattr.Min)
                {
                    MessageBox.Show(rangeattr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            cart = new ClsCart();

            if (cart.AddProductToCart(ProductID, Convert.ToInt32(textBox1.Text), Price) == false)
            {
                MessageBox.Show("Couldn't Add Product To Cart ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            this.Close();
        }

        private void EnterQuatity_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            textBox1.Focus();
        }
    }
}
