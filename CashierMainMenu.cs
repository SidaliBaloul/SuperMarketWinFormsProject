using SMBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class CashierMainMenu : Form
    {
        double _Total;
        public CashierMainMenu()
        {
            InitializeComponent();

            panel2.Controls.Add(ctrSearchProduct1);
            panel2.Controls.Add(ctrCart1);

            ctrSearchProduct1.Visible = true ;
            ctrCart1.Visible = false;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = false;

        }
        //DataTable dt = ClsProducts.GetProductsList();
        


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ctrSearchProduct1.Visible = true;
            ctrCart1.Visible = false;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = false;


            ctrSearchProduct1.RefreshData();

        }

        private void ctrSearchProduct1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrSearchProduct1.Visible = false ;
            ctrCart1.Visible = true;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = false;

            ctrCart1.Refreshdata();

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Gray;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Black;
        }

        private void CashierMainMenu_Load(object sender, EventArgs e)
        {
 

            ctrSearchProduct1.Visible = true;
            ctrSearchProduct1.RefreshData();

           

            ctrCart1.Visible = false;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = false;

            ctrCart1.Refreshdata();
            label5.Text = DateTime.Now.ToLongTimeString();
            ClsCart.databack += TotalAmount_DataBack;

            

        }

        private void TotalAmount_DataBack(double Total)
        {
            double total = Convert.ToDouble(label2.Text);
            total += Total;
            _Total = total;
            label2.Text = total.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Clear The Cart ? ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ClsCart.IsCartEmpty())
                {
                    MessageBox.Show("Cart Is Already Empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                ClsCart.ClearCart();
                label2.Text = "0";
                ctrCart1.Refreshdata();
            }
        }

        private void button3_Enter(object sender, EventArgs e)
        {
           
        }

        private void button3_Leave(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ctrSearchProduct1.Visible = false;
            ctrCart1.Visible = false;
            ctrchangepassword1.Visible = true;
            ctrSales1.Visible = false;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Gray;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Black;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Log Out ? ","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                
                this.Close();
                Login login = new Login();
                login.ShowDialog();
                
            }
        }

        private void ctrCart1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongTimeString();
            label7.Text = DateTime.Now.ToShortDateString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ctrSearchProduct1.Visible = false;
            ctrCart1.Visible = false;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = false;

            SettlePayment frm = new SettlePayment(_Total);
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ctrSearchProduct1.Visible = false;
            ctrCart1.Visible = false ;
            ctrchangepassword1.Visible = false;
            ctrSales1.Visible = true;

            ctrSales1.RefreshData();
        }

        private void ctrCart1_OnLoadCartList(double obj)
        {
            _Total = obj;
            label2.Text = _Total.ToString();
        }

        private void button8_Enter(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Gray;
        }

        private void button8_Leave(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Black;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Gray;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Black;
        }
    }
}
