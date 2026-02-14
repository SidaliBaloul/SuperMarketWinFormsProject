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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SuperMarket
{
    public partial class AddStockAdminForm : Form
    {
        double Total;

        public AddStockAdminForm()
        {
            InitializeComponent();
        }
        bool DegitsOnly = true;

        DataTable dt;
        DataTable dt2;

        private void FillProductsComboBox()
        {
             dt = ClsProducts.GetProductsListForAdmin();

            foreach(DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr[2]);
            }
        }

        private void FillSuppliersComboBox()
        {
            dt2 = ClsSupplier.GetSuppliersList();

            foreach (DataRow dr in dt2.Rows)
            {
                comboBox2.Items.Add(dr[1]);
            }
        }

        private void AddStockAdminForm_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(5);
            dateTimePicker1.MinDate = DateTime.Now.AddDays(10);

            FillProductsComboBox();
            FillSuppliersComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "".Trim() || Convert.ToInt16(textBox1.Text) < 30 )
            {
                MessageBox.Show("The Purchase Must Have A Quantity Of AtLeast 30 Units", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please Select Supplier !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show("Are You Sure You Want To Confirm The Purchase ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (ClsPurchases.AddPurchase(ClsProducts.FindByName(comboBox1.SelectedItem.ToString().Trim()),Convert.ToInt32(textBox1.Text),
                    Convert.ToDouble(textBox2.Text),Convert.ToDouble(label7.Text),ClsSupplier.FindByName(comboBox2.SelectedItem.ToString()),DateTime.Now,dateTimePicker1.Value) == false)
                {
                    MessageBox.Show("Something Went Off While Adding The Purchase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = ClsProducts.GetPriceOfAProduct(comboBox1.SelectedIndex + 1).ToString();
            textBox1.Enabled = true;
            label7.Text = "00";
            textBox1.Text = ""; 
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 5)
            {
                textBox1.Text = textBox1.Text.Remove(5, 1);
                return;
            }

            Total = Convert.ToInt16(textBox1.Text) * Convert.ToDouble(textBox2.Text);
            label7.Text = Total.ToString();
            
        }
    }
}
