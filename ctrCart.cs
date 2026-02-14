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
    public partial class ctrCart : UserControl
    {

        public event Action<double> OnLoadCartList;

        protected virtual void LoadCartList(double total)
        {
            Action<double> handler = OnLoadCartList;

            if(handler != null)
            {
                handler(total);
            }
        }

        public ctrCart()
        {
            InitializeComponent();
        }
        public DataTable dt;

        public async void Refreshdata()
        {
            var cartlist = await Task.Run(() =>
            {
                return ClsCart.GetCartList();
            });
 
            dataGridView1.DataSource = cartlist;

            if (OnLoadCartList != null)
                LoadCartList(ClsCart.GetCartProductsTotal());

        }

 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            switch(dataGridView1.CurrentCell.ColumnIndex)
            {
                case 0:

                    if ((int)dataGridView1.CurrentRow.Cells[5].Value < ClsProducts.GetProductQuantityAvailable((int)dataGridView1.CurrentRow.Cells[4].Value))
                    {
                        ClsCart cart = new ClsCart();
                        double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);

                        if (cart.AddProductToCart((int)dataGridView1.CurrentRow.Cells[4].Value, 1, price) == false)
                            MessageBox.Show("An Error Occured !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Refreshdata();
                    }
                    else
                        MessageBox.Show("You Achevied The Maximum Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;

                case 1:

                    if (MessageBox.Show("Are You Sure You Want To Delete This Product From Cart ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        if (ClsCart.DeleteProductFromCart((int)dataGridView1.CurrentRow.Cells[4].Value, Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) == false)
                            MessageBox.Show("An Error Occured While Deleting The Item ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Refreshdata();
                    break;

                case 2:

                    if ((int)dataGridView1.CurrentRow.Cells[5].Value != 1)
                    {
                        ClsCart cart2 = new ClsCart();
                        double price2 = Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);

                        if (cart2.AddProductToCart((int)dataGridView1.CurrentRow.Cells[4].Value, 1, price2, "REMOVE") == false)
                            MessageBox.Show("An Error Occured ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Refreshdata();
                        
                    }
                    else
                        MessageBox.Show("You Can't Go Below 1 ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }
        }
    }
}
