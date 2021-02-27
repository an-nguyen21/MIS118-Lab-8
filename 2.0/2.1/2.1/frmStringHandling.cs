
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2._1
{
    public partial class frmStringHandling : Form
    {
        public frmStringHandling()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Method that closes form
            this.Close();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            //Store user email in variable
            string email = txtEmail.Text;

            //If statement to see if email address has @ symbol
            if (email.Contains("@"))
            {
                //Removes leading and trailing spaces
                email = email.Trim();

                //Make array to store split strings
                string[] ArrayEmail = email.Split(Convert.ToChar("@"));

                string userName = ArrayEmail[0];
                string domain = ArrayEmail[1];

                //Show user name and domain in message box
                MessageBox.Show("Username: " + userName + "\n" + "Domain name: " + domain, "Parsed String");
            }

            //Error message if email does not contain @
            else
            {
                MessageBox.Show("Email must contain @ symbol.", "Error");
            }
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            //formating string and converting state to uppercase
            string format = "CityStateZip" + txtCity.Text + ", " + txtState.Text.ToUpper() + " " + txtZip.Text;
            //using Insert() method to insert commas and spaces
            format = format.Insert(4, ",").Insert(5, " ").Insert(11, ",").Insert(12, " ").Insert(16, ":").Insert(17, " ");
            //Display on the message box
            MessageBox.Show(format, "Formatted String");
        }
    }
}
