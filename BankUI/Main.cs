using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            button1.Text = "User Registration";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegistrationForm();
            registrationForm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var userLoginForm = new UserLoginForm();
            userLoginForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var adminLoginForm = new AdminLoginForm();
            adminLoginForm.ShowDialog();
        }
    }
}
