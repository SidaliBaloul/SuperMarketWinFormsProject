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
    public partial class CashiersSuppliersAdminForm : Form
    {
        public CashiersSuppliersAdminForm()
        {
            InitializeComponent();
        }

        DataTable dt;

        public async void RefreshData()
        {
            dt = await Task.Run(() => { return ClsSupplier.GetSuppliersList(); });
            dataGridView1.DataSource = dt;
        }

        private void CashiersSuppliersAdminForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Search For Supplier Name";
            textBox1.ForeColor = Color.Gray;
            RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSupplierForm frm = new AddSupplierForm(-1);
            frm.ShowDialog();

            RefreshData();
        }

        private void updateInfosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSupplierForm frm = new AddSupplierForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            RefreshData();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Remove This Supplier ? ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ClsSupplier.RemoveSupplier((int)dataGridView1.CurrentRow.Cells[0].Value) == false)
                {
                    MessageBox.Show("An Error Has Occured While Removing The Supplier ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                    RefreshData();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search For Supplier Name")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "Search For Supplier Name";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dt != null && textBox1.Text != "Search For Supplier Name")
            {
                if (textBox1.Text.Trim() == "")
                {
                    dt.DefaultView.RowFilter = "";
                    dataGridView1.DataSource = dt;

                    return;
                }

                var filter = await Task.Run(() =>
                {
                    return string.Format("[{0}] LIKE '{1}%'", "Name", textBox1.Text);
                });


                dt.DefaultView.RowFilter = filter;
                dataGridView1.DataSource = dt;
            }
        }
    }
}
