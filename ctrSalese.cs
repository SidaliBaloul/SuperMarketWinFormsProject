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
    public partial class ctrSalese : UserControl
    {
        public ctrSalese()
        {
            InitializeComponent();
        }

        private DataTable dt;

        public async void RefreshData()
        {
            var Sales = await Task.Run(() => {return ClsSales.GetSalesList();});
            dataGridView1.DataSource = Sales;

            var TotalSales = await Task.Run(() => { return ClsSales.GetTotalSales(); });

            label4.Text = TotalSales.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                SaleDetails frm = new SaleDetails((int)dataGridView1.CurrentRow.Cells[1].Value);
                frm.ShowDialog();
            }
        }

        private void ctrSales_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                
            }
            else
                dataGridView1.DataSource = ClsSales.FilterDataByDate(comboBox1.SelectedIndex);
            

        }
    }
}
