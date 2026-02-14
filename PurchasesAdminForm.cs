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
    public partial class PurchasesAdminForm : Form
    {
        public PurchasesAdminForm()
        {
            InitializeComponent();
        }

        DataTable dt;
        DataTable dt2;

        private async void RefreshData()
        {
            dt = await Task.Run(() => { return ClsPurchases.GetPurchasesList(); });
            dt2 = await Task.Run(() => { return ClsPurchases.GetPurchasesToStockIn(); });


            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt2;
        }

        private void PurchasesAdminForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStockAdminForm frm = new AddStockAdminForm();
            frm.ShowDialog();

            RefreshData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To StockIn ? ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("There Is Nothing To Stock In !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    ClsPurchases.StockIn(ClsProducts.FindByName(row.Cells[1].Value.ToString()), (int)row.Cells[3].Value, (DateTime)row.Cells[7].Value);
                    ClsPurchases.UpdateStockedInStatus((int)row.Cells[0].Value);
                }

                RefreshData();
            }
        }
    }
    //dataGridView2.CurrentRow.Cells[1].Value.ToString()
}
