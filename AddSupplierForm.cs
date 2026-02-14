using SMBusinessLayer;
using SuperMarket.Properties;
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
    public partial class AddSupplierForm : Form
    {
        public enum eMode { Add = 1, Update = 2 }
        public eMode mode;


        bool DegitsOnly = true;
        ClsSupplier supplier;
        int SupplierID;

        public AddSupplierForm(int supplierID)
        {
            InitializeComponent();
            
            if(supplierID != -1)
                this.mode = eMode.Update;
            else
                this.mode = eMode.Add;

            SupplierID = supplierID;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim())
                && !string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                if (mode == eMode.Add)
                    supplier = new ClsSupplier();

                supplier.Name = textBox1.Text.Trim();
                supplier.Phone = Convert.ToDecimal(textBox2.Text);
                supplier.Email = textBox3.Text.Trim();

                if (mode == eMode.Add)
                {
                    if (supplier.AddSupplier())
                        MessageBox.Show("Supplier Added Succesfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("An Error Has Occured While Adding Supplier ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (supplier.UpdateSupplier(SupplierID, supplier.Phone, supplier.Email))
                        MessageBox.Show("Supplier Updated Succefully Succesfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("An Error Has Occured While Updating Supplier ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Close();
            }
            else
                MessageBox.Show("You Have To Fill All Data ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void LoadData()
        {
            if(mode == eMode.Update)
            {
                supplier = ClsSupplier.Find(SupplierID);

                textBox1.Text = supplier.Name;
            }
        }

        private void AddSupplierForm_Load(object sender, EventArgs e)
        {
            if(mode == eMode.Add)
            {
                pictureBox1.Image = Resources.create_account;
                textBox1.ReadOnly = false;
                button1.Text = "Add Supplier";
            }
            else
            {
                pictureBox1.Image = Resources.updated;
                textBox1.ReadOnly = true;
                button1.Text = "Update Supplier";

                LoadData();
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
           
        }
    }
}
