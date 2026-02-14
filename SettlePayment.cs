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
    public partial class SettlePayment : Form
    {
        private double _Total;
        private bool DegitsOnly = true;

        public SettlePayment(double Total)
        {
            InitializeComponent();

            _Total = Total;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox3.Text += "1";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox3.Text += "2";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox3.Text += "3";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox3.Text += "00";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text += "4";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text += "5";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text += "6";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text += "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text += "7";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox3.Text += "8";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text += "9";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void SettlePayment_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button13;
            textBox3.Focus();
            textBox1.Text = _Total.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = (Convert.ToDouble(textBox3.Text) - Convert.ToDouble(textBox1.Text)).ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DegitsOnly)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please Enter The Given Amount ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Convert.ToDouble(textBox3.Text) < Convert.ToDouble(textBox1.Text) )
            {
                MessageBox.Show("Amount Given By Client Is Insufficient ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (_Total == 0)
            {
                MessageBox.Show("You Have To Add At Least One Product To Cart ","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClsSales.CompleteSale(_Total);
            this.Close();

        }
    }
}
