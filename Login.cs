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
    public partial class Login : Form
    {
        ClsUser user;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields Are Not Validated !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            user = ClsUser.GetUserByUserNameAndPassword(textBox1.Text.Trim(),textBox2.Text.Trim());
           //ClsUtile.CreateAnEventSourceForEventLogs("SuperMarketApp");


            if (user == null )
            {
                MessageBox.Show("Wrong UserName OR Password. Try Again !","Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //ClsUtile.LogAnEventToEventLogs("SuperMarketApp", "Login Failed", System.Diagnostics.EventLogEntryType.Warning);
                return;
            }

            this.Hide();

            

            ClsCurrentUser.UserID = user.UserID;
            ClsCurrentUser.UserName = user.UserName;
            ClsCurrentUser.Password = user.Password;

            if (checkBox2.Checked)
                ClsUtile.JSONSerialization(textBox1.Text);

            if (user.UserName == "Cashier")
            {
                CashierMainMenu frm = new CashierMainMenu();
                frm.ShowDialog();
            }
            else
            {
                MainScreenAdmin frm = new MainScreenAdmin();
                frm.ShowDialog(); 
            }

            
            //ClsUtile.LogAnEventToEventLogs("SuperMarketApp", "Loged in Succesfully", System.Diagnostics.EventLogEntryType.Information);
            //this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if(ClsUtile.JSONDeserilization() != null)
                textBox1.Text = ClsUtile.JSONDeserilization();

            this.AcceptButton = button1;
            textBox1.Focus();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "UserName Cannot Be Blank");
                return;
            }
            else
                errorProvider1.SetError(textBox1, null);
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox2, "Password Cannot Be Blank");
                return;
            }
            else
                errorProvider1.SetError(textBox2, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
