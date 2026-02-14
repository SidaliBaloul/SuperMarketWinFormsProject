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
    public partial class ctrchangepassword : UserControl
    {
        public ctrchangepassword()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Something's Wrong :( ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if(textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("Please Confirm The Right Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ClsUser.IsPasswordCorrect(ClsCurrentUser.UserID,textBox1.Text.Trim()))
            {
                ClsUser.UpdateUserPassword(ClsCurrentUser.UserID, textBox2.Text.Trim());
                MessageBox.Show("Password Updated Succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Please Enter The Right Current Password ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Current Password Can't Be Blank !");
                return;
            }
            else
                errorProvider1.SetError(textBox1, null);

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox2, "New Password Can't Be Blank !");
                return;
            }
            else
                errorProvider1.SetError(textBox2, null);
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox3, "Confirm Password Can't Be Blank !");
                return;
            }
            else
                errorProvider1.SetError(textBox3, null);

           
        }
    }
}
