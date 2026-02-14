using SMBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class UpdateProductAdminForm : Form
    {

        decimal BarCode {  get; set; }
        string ProductName { get; set; }
        double Price { get; set; }

        int ProductID { get; set; }

        private bool DegitsOnly = true;

        ClsProducts product;

        public UpdateProductAdminForm(int productid)
        {
            InitializeComponent();

            this.ProductID = productid;
        }

        private void UpdateProductAdminForm_Load(object sender, EventArgs e)
        {
            product = ClsProducts.Find(ProductID);

            if (product == null)
            {
                MessageBox.Show("Something Went Off While Implementing Product Object","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            label2.Text = product.Name;
            textBox1.Text = product.Price.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DegitsOnly)
            {
                TextBox tb = sender as TextBox;

                // Allow control keys (Backspace, etc.)
                if (char.IsControl(e.KeyChar))
                    return;

                // Allow digits
                if (char.IsDigit(e.KeyChar))
                    return;

                // Allow comma (only one)
                if (e.KeyChar == ',' && !tb.Text.Contains(","))
                    return;

                // Block everything else
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
            if(textBox1.Text == "" ||  textBox2.Text == "")
            {
                MessageBox.Show("Please Fill All Data !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(textBox2.Text.Length < 13)
            {
                MessageBox.Show("BarCode Must Have 13 Degits !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are You Sure You Want To Update This Product Infos ?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(ClsProducts.IsBarCodeAlreadyExists(textBox2.Text))
                {
                    MessageBox.Show("BarCode Already Exists Please Enter Another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ClsProducts.UpdateProductInfos(ProductID,textBox2.Text,Convert.ToDouble(textBox1.Text)) == false)
                {
                    MessageBox.Show("Something Went Off While Updating The Product Infos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 13)
            {
                textBox2.Text = textBox2.Text.Remove(13,1);
            }
        }
    }
}
