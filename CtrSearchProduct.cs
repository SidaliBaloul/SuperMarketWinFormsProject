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
    public partial class CtrSearchProduct : UserControl
    {
        public CtrSearchProduct()
        {
            InitializeComponent();
        }

        public DataTable Datatable;


        public async void RefreshData()
        {
            Datatable = await Task.Run(() =>
            {
                return ClsProducts.GetProductsList();
            });

            dataGridView1.DataSource = Datatable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                EnterQuatity frm = new EnterQuatity((int)dataGridView1.CurrentRow.Cells[5].Value, 
                    (int)dataGridView1.CurrentRow.Cells[1].Value, Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value));


                frm.ShowDialog();
            }
        }

        private void CtrSearchProduct_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Search For Product";
            textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "Search For Product")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "Search For Product";
                textBox1.ForeColor = Color.Gray;
                RefreshData();
            }
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

                var filter = await Task.Run(() =>
                {
                    return string.Format("[{0}] LIKE '{1}%'", "Name", textBox1.Text);
                });


                Datatable.DefaultView.RowFilter = filter;
                dataGridView1.DataSource = Datatable;
            }
            
        }
    }
}
