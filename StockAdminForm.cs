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
    public partial class StockAdminForm : Form
    {
        public StockAdminForm()
        {
            InitializeComponent();
        }

        DataTable dt;

        public async void RefreshData()
        {
            dt = await Task.Run(() => { return ClsStock.GetProductsInStock(); });
            dataGridView1.DataSource = dt;
        }

        private void StockAdminForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
