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
    public partial class MainScreenAdmin : Form
    {
        public MainScreenAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductsListAdminForm frm = new ProductsListAdminForm();
            frm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StockAdminForm frm = new StockAdminForm();
            frm.ShowDialog();
        }

        private void productsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SalesListAdminForm frm = new SalesListAdminForm();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CashiersSuppliersAdminForm frm = new CashiersSuppliersAdminForm();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PurchasesAdminForm frm = new PurchasesAdminForm();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Log Out ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
                Login login = new Login();
                login.ShowDialog();

            }
        }
    }
}
