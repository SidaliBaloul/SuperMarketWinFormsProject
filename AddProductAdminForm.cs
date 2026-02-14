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
    public partial class AddProductAdminForm : Form
    {
        private bool DegitsOnly = true;

        public AddProductAdminForm()
        {
            InitializeComponent();
        }

        private void AddProductAdminForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 13)
            {
                textBox2.Text = textBox2.Text.Remove(13, 1);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "".Trim())
            {
                MessageBox.Show("Please Fill All Data !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text.Length < 13)
            {
                MessageBox.Show("BarCode Must Have 13 Degits !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var properity = typeof(ClsProducts).GetProperty("Price");
            var rangeattr = properity.GetCustomAttribute<RangeCustomAttribute>();

            if(rangeattr != null)
            {
                if(Convert.ToDouble(textBox3.Text.Trim()) < rangeattr.Min || Convert.ToDouble(textBox3.Text.Trim()) > rangeattr.Max)
                {
                    MessageBox.Show(rangeattr.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (MessageBox.Show("Are You Sure You Want To Add This Product ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ClsProducts.IsBarCodeAlreadyExists(textBox2.Text))
                {
                    MessageBox.Show("BarCode Already Exists Please Enter Another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool add = await Task.Run(() => { return ClsProducts.AddProduct(textBox1.Text, textBox2.Text, Convert.ToDouble(textBox3.Text)); });

                if (add == false)
                {
                    MessageBox.Show("Something Went Off While Adding The Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Close();
            }
        }
    }
}
