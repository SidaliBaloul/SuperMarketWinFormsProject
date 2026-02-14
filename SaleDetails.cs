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
    public partial class SaleDetails : Form
    {
        DataTable dt;
        private int _SaleID = 0;
        public SaleDetails(int SaleID)
        {
            InitializeComponent();

            this._SaleID = SaleID;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dt != null)
            {
                if (textBox1.Text.Trim() == "")
                {
                    dt.DefaultView.RowFilter = "";
                    return;
                }

                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", "Name", textBox1.Text);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RefreshData()
        {
            dt = ClsSales.GetSalesDetailsList(_SaleID);
            dataGridView1.DataSource = dt;
        }

        private void SaleDetails_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Search For Product";
            textBox1.ForeColor = Color.Gray;
            RefreshData();
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
    }
}
