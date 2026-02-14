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
    public partial class ProductsListAdminForm : Form
    {
        public ProductsListAdminForm()
        {
            InitializeComponent();
        }

        private void ctrProductsListAdminForm1_Load(object sender, EventArgs e)
        {

        }
        public DataTable Datatable;

        public async void RefreshData()
        {
            Datatable = await Task.Run(() => { return ClsProducts.GetProductsListForAdmin(); });
            dataGridView1.DataSource = Datatable;
        }

        private void ProductsListAdminForm_Load(object sender, EventArgs e)
        {
            
            textBox1.Text = "Search For Product";
            textBox1.ForeColor = Color.Gray;
            RefreshData();
        }

        private void updateProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateProductAdminForm frm = new UpdateProductAdminForm((int)dataGridView1.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            RefreshData();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Datatable != null)
            {
                if (textBox1.Text.Trim() == "")
                {
                    Datatable.DefaultView.RowFilter = "";
                    dataGridView1.DataSource = Datatable;

                    return;
                }

                var filter = await Task.Run(() => { return string.Format("[{0}] LIKE '{1}%'", "Name", textBox1.Text); });

                Datatable.DefaultView.RowFilter = filter;
                dataGridView1.DataSource = Datatable;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "Search For Product";
                textBox1.ForeColor = Color.Gray;
                RefreshData();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search For Product")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void removeProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProductAdminForm frm = new AddProductAdminForm();
            frm.ShowDialog();

            RefreshData();
        }
    }
}
