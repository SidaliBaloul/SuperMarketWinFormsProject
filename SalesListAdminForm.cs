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
    public partial class SalesListAdminForm : Form
    {
        public SalesListAdminForm()
        {
            InitializeComponent();
        }

        private void ctrSales1_Load(object sender, EventArgs e)
        {

        }

        private void SalesListAdminForm_Load(object sender, EventArgs e)
        {
            ctrSalese1.RefreshData();
        }

        private void ctrSalese1_Load(object sender, EventArgs e)
        {

        }
    }
}
